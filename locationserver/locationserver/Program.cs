using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace locationserver
{
    class Program
    {
        //The public read and write variables allow the form to change the value
        public static int readT = 1000;
        public static int writeT = 1000;
        //This object allows us to use the filepath based on user inpout
        public static Logging Log;
        //This variable lets us know if we will use the interface
        static bool Interface = false;

        /// <summary>
        /// The Main Method handels the Client inputs/Arguments
        /// This inclued Error handling 
        /// </summary>
        public static void Main(string[] args)
        {
            string filename = null;
            for (int i = 0; i < args.Length; ++i)
            {
                switch (args[i])
                {
                    case "-w": //Checks to see if the user wants to use the user interface or console
                        Interface = true;
                        break;
                    case "-l": //Checks to see if the user wants to log files
                        filename = args[++i];
                        break;
                    default:
                        Console.WriteLine("Unknown Option " + args[i]);
                        break;
                }
            }
            Log = new Logging(filename); //Create and object and pass the given filename
            if (Interface == false)//if -w is not in the argument then we dont use the UI
            {
                runServer();
            }
            else //else start up the UI
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false); 
                Application.Run(new ServerForm());
            }
        }
        static Dictionary<string, string> client = new Dictionary<string, string>(); //This dictionary will hold all the clients and locations

        /// <summary>
        /// When this method is called we run the server and wait for the client to connect
        /// </summary>
        public static void runServer()
        {
            TcpListener listener;
            Socket connection;
            Handler RequestHandler;
            try
            {
                listener = new TcpListener(IPAddress.Any, 43); //our listener will be in port 43 and will have any IP
                listener.Start(); //Listener begins to listen for client connections
                Console.WriteLine("Server Listening");
                while (true) //infinite loop
                {
                    //When Client connect we accept the client
                    connection = listener.AcceptSocket();
                    RequestHandler = new Handler();
                    //We create a thread that allows multipe clients to connect and the same time
                    Thread t = new Thread(() => RequestHandler.doRequest(connection, Log));
                    t.Start();
                    
                }
            }
            catch (Exception e)
            {
                //If there is an issue with the connection we catch the error so that the program does not crash
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// This means Handles the user inputs and acts based on user request
        /// </summary>
        public class Handler
        {
            public void doRequest(Socket connection, Logging Log)
            {
                string Host = ((IPEndPoint)connection.RemoteEndPoint).Address.ToString(); //We get the ip of the client
                NetworkStream socketStream;
                socketStream = new NetworkStream(connection);
                Console.WriteLine("Client Connected!");
                string line = null;
                string Status = "OK";
                try
                {
                    StreamWriter sw = new StreamWriter(socketStream);
                    StreamReader sr = new StreamReader(socketStream);

                    //Auto flush will automaticly flush without me telling it to
                    sw.AutoFlush = true;
                    //readT and writeT are the timeouts which we get from the form
                    socketStream.ReadTimeout = readT;
                    socketStream.WriteTimeout = writeT;


                    //Read the first line
                    line = sr.ReadLine();//We use this line to tell which protocal the client is using 

                    //Console.WriteLine("Response Recieved: "+line);

                    string[] splitRead = line.Split(new char[] { '/' }, 2); //Splits the input by the slash to get the username

                    //This if statment checks to see what protocol the client is using.
                    //Based on the protocol the location and username will be in different places
                    //HTTP 1.1 Lookup
                    if (line.StartsWith("GET /?name=") && line.EndsWith(" HTTP/1.1"))
                    {
                        string[] splitRead1 = line.Split(new char[] { ' ' });
                        sr.ReadLine();
                        string username = splitRead1[1].Remove(0, 7);

                        if (client.ContainsKey(username))
                        {
                            string location = client[username];
                            sw.WriteLine("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n" + location + "\r\n");
                        }
                        else
                        {
                            sw.WriteLine("HTTP/1.1 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
                            Status = "UNKOWN";
                        }
                    }
                    //HTTP 1.1 Update
                    else if (line.StartsWith("POST / HTTP/1.1") && (sr.Peek() >= 0))
                    {
                        string[] location;
                        sr.ReadLine();//Read multiple lines to get to the last line which contains username and location
                        sr.ReadLine();
                        sr.ReadLine();
                        line = sr.ReadLine();//This line holds both the username and location
                        string[] splitRead1 = line.Split(new char[] { '&' }, 2);//Spliting the line so we are left with username and location
                        location = splitRead1[1].Split(new char[] { '=' }, 2);
                        string[] username = splitRead1[0].Split(new char[] { '=' }, 2);


                        if (client.ContainsKey(location[1]))
                        {
                            client[username[1]] = location[1]; //Save new location to client
                            sw.WriteLine("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                        }
                        else
                        {
                            client[username[1]] = location[1];
                            sw.WriteLine("HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                        }
                    }
                    //HTTP 1.0 Lookup
                    else if (line.StartsWith("GET /?") && line.EndsWith(" HTTP/1.0"))
                    {
                        string[] splitRead1 = line.Split(new char[] { '?' }, 2);
                        string[] username = splitRead1[1].Split(new char[] { ' ' }, 2);

                        if (client.ContainsKey(username[0]))
                        {
                            sw.WriteLine("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n" + client[username[0]] + "\r\n");
                        }
                        else
                        {
                            sw.WriteLine("HTTP/1.0 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
                            Status = "UNKNOWN";
                        }
                    }
                    //HTTP 1.0 Update
                    else if (line.StartsWith("POST /") && (sr.Peek() >= 0) && line.EndsWith(" HTTP/1.0"))
                    {
                        //Read multiple lines to get to the last line which contains username and location
                        sr.ReadLine();
                        sr.ReadLine();
                        string lastLine = sr.ReadLine();
                        string[] m = splitRead[1].Split(new char[] { ' ' }, 2);
                        string username = m[0];

                        if (client.ContainsKey(username))
                        {
                            client[username] = lastLine;
                            sw.WriteLine("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                        }
                        else
                        {
                            client.Add(username, lastLine);
                            sw.WriteLine("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                        }
                    }

                    //HTTP 0.9 Lookup
                    else if (line.StartsWith("GET /"))
                    {
                        if (client.ContainsKey(splitRead[1]))//Check to see if we have the client in the dictionary
                        {
                            sw.WriteLine("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n" + client[splitRead[1]] + "\r\n");
                        }
                        else //if user is not in dictionary send and error message to client
                        {
                            sw.WriteLine("HTTP/0.9 404 Not Found\r\nContent-Type: text/plain\r\n\r\n");
                            Status = "Unknown";
                        }
                    }
                    //HTTP 0.9 Update
                    else if (line.StartsWith("PUT /") && (sr.Peek() >= 0))
                    {
                        sr.ReadLine();
                        string lastLine = sr.ReadLine(); //last line contains the location when using this protocol
                        if (client.ContainsKey(splitRead[1]))
                        {
                            client[splitRead[1]] = lastLine;
                            sw.WriteLine("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n" + lastLine);
                        }
                        else
                        {
                            client.Add(splitRead[1], lastLine);
                            sw.WriteLine("HTTP/0.9 200 OK\r\nContent-Type: text/plain\r\n\r\n");
                        }
                    }
                    //Whois request
                    else
                    {
                        string[] spaceSplitRead = line.Split(new char[] { ' ' }, 2);
                        //Lookup
                        if (spaceSplitRead.Length == 1) //if length is one this means just username
                        {
                            if (client.ContainsKey(spaceSplitRead[0]))//checks to see if we have the client in dictionary
                            {
                                sw.WriteLine(client[spaceSplitRead[0]] + "\r\n");
                            }
                            else // Gives error
                            {
                                sw.WriteLine("ERROR: no entries found");
                                Status = "Unknown";
                            }
                        }
                        //Update
                        else if (spaceSplitRead.Length == 2)//username and location
                        {
                            if (client.ContainsKey(spaceSplitRead[0])) //if client is in dictionary update location
                            {
                                client[spaceSplitRead[0]] = spaceSplitRead[1];
                                sw.WriteLine("OK\r\n");
                            }
                            else // else create new client with new location
                            {
                                client.Add(spaceSplitRead[0], spaceSplitRead[1]);
                                sw.WriteLine("OK\r\n");
                            }
                        }

                    }
                }
                catch (Exception)
                {
                    //This is to catch any exception that would crash the server
                    Console.WriteLine("ERORR!!!!");
                    Status = "EXCEPTION";
                }
                finally
                {
                    //Now the request is complete close in the sockets as they are n longer needed
                    socketStream.Close();
                    connection.Close();
                    Log.WriteToLog(Host, line, Status);
                }

            }
        }


        
            
    }

}

/// <summary>
/// Logging Example from https://stackoverflow.com/questions/2954900/simple-multithread-safe-log-class
/// </summary>
public class Logging
{
    //Logfile is null therefore write to console
    public static string LogFile = null;
    /// <summary>
    /// This creates the logfile at the specified name
    /// </summary>
    /// <param name="filename"></param>
    public Logging(string filename)
    {
        LogFile = filename;
    }

    private static readonly object locker = new object();

    /// <summary>
    /// This write a log entry wo the console andoptionally to a file
    /// this also traps file erros/execptions
    /// </summary>
    /// <param name="hostname"></param>
    /// <param name="message"></param>
    /// <param name="status"></param>
    public void WriteToLog(string hostname, string message, string status)
    {
        //Creates a line in common log format
        string line = hostname + " - - " + DateTime.Now.ToString("'['dd'/'mm'/'yyyy'/'':'HH':'mm':'ss zz00']'") + " \"" + message + "\" " + status;
        //lock the file write to prevent concurrent threaded writes
        lock (locker)
        {
            Console.WriteLine(line);
            if (LogFile == null) return;
            //if there isnt a log file will exit after writing to console
            try
            {
                StreamWriter sw;
                sw = File.AppendText(LogFile);
                sw.WriteLine(line);
                sw.Close();
            }
            catch
            {
                Console.WriteLine("Unable to Write Log File " + LogFile);
            }
        }
    }
}