//Demonstrate Sockets
using System;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace location
{
    public class Whois
    {
        //The server response and debug reponse are public so that the form 
        //can access them and use the in the form textBox
        public static string serverResponse;
        public static string debugResponse;

        /// <summary>
        /// The main method is used to manage the arguments and see if they are valid
        /// </summary>
        /// <param name="args"></param>
        static public void Main(string[] args)
        {
            //Asinging the defualt variables
            string hostName = "whois.net.dcs.hull.ac.uk";
            int port = 43;
            string protocol = "whois";
            bool debug = false;
            int timeout = 1000;
            string username = null;
            string location = null;
            string response;

            if (args.Length == 0) //If args equal 0 then we open the UI
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ClientForm());
            }
            else //Else we go through every cell in the array and proccess the info
            {
                for (int i = 0; i < args.Length; i++)
                {
                    try
                    {
                        switch (args[i])
                        {
                            case "-h":
                                hostName = args[i + 1];
                                i++;
                                break;
                            case "-p":
                                port = int.Parse(args[i + 1]);
                                i++;
                                break;
                            case "-d":
                                debug = true;
                                break;
                            case "-t":
                                timeout = int.Parse(args[i + 1]);
                                i++;
                                break;
                            case "-h0":
                                protocol = args[i];
                                break;
                            case "-h1":
                                protocol = args[i];
                                break;
                            case "-h9":
                                protocol = args[i];
                                break;
                            case "zna!!@":
                                protocol = "whois";
                                break;
                            default:
                                if (username == null)
                                    username = args[i];
                                else if (location == null)
                                    location = args[i];
                                else
                                {
                                    Console.WriteLine("ERROR: too many arguments");
                                    return;
                                }
                                break;
                        }
                    }
                    catch (Exception e) //This exception is through if there is a argument image
                    {
                        Console.WriteLine("Args Error: " + e);
                    }
                }

            

            }
            if (username == null) //if username equals null then we can do anything
            {
                Console.WriteLine("ERROR: too few arguments");
                return;
            }
            if (debug)//if the user inputed -d then we output the user inputs
            {
                Console.WriteLine($"Username: {username}\r\nLocation: {location}\r\nHost Name: {hostName}\r\nPort Number: {port}\r\nTimeout: SOON!");
                debugResponse = ($"Username: {username}\r\nLocation: {location}\r\nHost Name: {hostName}\r\nPort Number: {port}\r\nTimeout: {timeout}");
            }
            try
            {
                //Now we start proccesing the infor based on client input
                TcpClient client = new TcpClient();//Create a client object
                client.Connect(hostName, port);//get user port and host name
                StreamWriter sw = new StreamWriter(client.GetStream());
                StreamReader sr = new StreamReader(client.GetStream());

                //timout is a public variable that can be accessed by the form to allow the user to input a timeout
                client.ReceiveTimeout = timeout;
                client.SendTimeout = timeout;
                
                switch (protocol)//Based on the protocol we do different things
                {
                    case "-h9"://http 0.9
                        if (location == null)
                        {
                            // perform the lookup request
                            sw.WriteLine("GET /" + username); //Send server what we want e.g. GET /username
                            sw.Flush();//Force send to server
                            response = sr.ReadToEnd(); //Read server response
                            //Split the server respons and get just the location
                            string[] lines = response.Split(new string[] { "\r\n" }, StringSplitOptions.None); 
                            location = lines[3];
                            //Write to console different based on server response
                            if (response.Contains("HTTP/0.9 404 Not Found\r\nContent-Type: text/plain\r\n\r\n"))
                            {
                                Console.WriteLine(response);
                                //Add console.writeline to public variable serverresponse so that we can add it to form text box
                                serverResponse = response;
                            }
                            else
                            {
                                Console.WriteLine(username + " is " + location);
                                serverResponse = username + " is " + response;
                            }

                        }
                        else
                        {
                            // perform the update request
                            sw.WriteLine("PUT /" + username + "\r\n\r\n" + location);
                            sw.Flush();
                            response = sr.ReadToEnd();
                            string[] lines = response.Split(new char[] { ' ' }, 3);

                            if (response.Contains(lines[2]))
                            {
                                Console.WriteLine(username + " location changed to be " + location);
                                serverResponse = username + " location changed to be " + location;
                            }
                            else
                            {
                                Console.WriteLine("Error!");
                                serverResponse = "Error!";
                            }
                        }
                        break;
                    case "-h0": //http 1.0
                        if (location == null)
                        {
                            // perform the lookup request
                            sw.WriteLine("GET /?" + username + " HTTP/1.0\r\n");
                            sw.Flush();

                            response = sr.ReadToEnd();
                            string[] lines = response.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                            location = lines[3];
                            if (response.Contains("HTTP/1.0 404 Not Found\r\nContent-Type: text/plain\r\n\r\n"))
                            {
                                Console.WriteLine(response);
                                serverResponse = response;
                            }
                            else
                            {
                                Console.WriteLine(username + " is " + location+"\r\n");
                                serverResponse = username + " is " + location;
                            }
                        }
                        else
                        {
                            // perform the update request
                            sw.WriteLine("POST /" + username + " HTTP/1.0\r\nContent-Length: " + location.Length + "\r\n\r\n" + location);
                            sw.Flush();

                            if (sr.ReadLine().Contains("HTTP/1.0 200 OK"))
                            {
                                Console.WriteLine(username + " location changed to be " + location);
                                serverResponse = username + " location changed to be " + location;
                            }
                            else
                            {
                                Console.WriteLine("Error!");
                                serverResponse = "Error!";
                            }
                        }
                        break;
                    case "-h1"://http 1.1
                        if (location == null && port != 80)
                        {
                            // perform the lookup request
                            sw.WriteLine("GET /?name=" + username + " HTTP/1.1\r\nHost: " + hostName + "\r\n");
                            sw.Flush();

                            response = sr.ReadToEnd();
                            string[] lines = response.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                            location = lines[3];

                            if (response == "HTTP/1.1 404 Not Found\r\nContent-Type: text/plain\r\n\r\n")
                            {
                                Console.WriteLine(response);
                                serverResponse = response;
                            }
                            else
                            {
                                Console.WriteLine(username + " is " + location + "\r\n");
                                serverResponse = username + " is " + location;
                            }

                        }
                        else if (location == null && port == 80) //This if statment handles the html responce
                        {
                            sw.WriteLine("GET /?name=" + username + " " + "HTTP/1.1\r\n" + "Host: " + hostName + "\r\n\r\n");
                            sw.Flush();
                            response = sr.ReadLine();

                            //This is how we handel http responses
                            List<string> htmlList = new List<string>();
                            while (sr.Peek() > -1)
                            {
                                response = sr.ReadLine();
                                htmlList.Add(response);
                            }

                            response = "";
                            int spaceIndex = htmlList.IndexOf("");

                            for (int i = spaceIndex + 1; i < htmlList.Count; i++)
                            {
                                response += htmlList[i];
                                response += "\r\n";
                            }

                            Console.WriteLine(username + " is " + response);
                            serverResponse = username + " is " + response;
                        }
                        else
                        {
                            // perform the update request
                            int length = 15 + username.Length + location.Length;
                            sw.WriteLine("POST / HTTP/1.1\r\nHost: " + hostName + "\r\nContent-Length: " + length + "\r\n\r\nname=" + username + "&location=" + location);
                            sw.Flush();
                            response = sr.ReadLine();
                            if (response.Contains("OK"))
                            {
                                Console.WriteLine(username + " location changed to be " + location);
                                serverResponse = username + " location changed to be " + location;
                            }
                            else
                            {
                                Console.WriteLine("Error!");
                                serverResponse = "Error!";
                            }
                        }
                        break;
                    case "whois": //defualt whois protocol
                        if (location == null)
                        {
                            // perform the lookup request
                            sw.WriteLine(username);
                            sw.Flush();
                            response = sr.ReadToEnd();
                            if (response != "ERROR: no entries found")
                            {
                                Console.WriteLine(username + " is " + response);
                                serverResponse = username + " is " + response;

                            }
                            else
                            {
                                Console.WriteLine("Error!");
                                serverResponse = "Error!";
                            }

                        }
                        else
                        {
                            // perform the update request
                            sw.WriteLine(username + " " + location);
                            sw.Flush();
                            response = sr.ReadLine();
                            if (response.Contains("OK"))
                            {
                                Console.WriteLine(username + " location changed to be " + location);
                                serverResponse = username + " location changed to be " + location;
                            }
                            else
                            {
                                Console.WriteLine("Error!2");
                                serverResponse = "Error!2";
                            }
                        }

                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error!" + e);
            }
        }

    }
}

