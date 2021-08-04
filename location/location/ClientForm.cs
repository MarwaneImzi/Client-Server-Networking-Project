using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace location
{
    public partial class ClientForm : Form
    {
        //Settiing all the variables so that they can be used in multiple methods
        string hostName;
        string port;
        string protocol;
        string username;
        string location;
        string debug;
        string timeout;
        
        public ClientForm()
        {
            InitializeComponent();
            //Code bellow is for Box Design 
            SendBox.FlatStyle = FlatStyle.Flat;
            SendBox.FlatAppearance.BorderSize = 0;
        }

        private void serverNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (serverNameTextBox.Text == "")
            {
                hostName = "whois.net.dcs.hull.ac.uk";
            }
            else
            {
                hostName = serverNameTextBox.Text;
            }
        }

        private void SendBox_Click(object sender, EventArgs e)
        {
            //When the button is clicked we create an array that we send to the main
            string[] args = { "-t", timeout, "-h", hostName, "-p", port, protocol, debug, username, location };
            Whois.Main(args);
            textBox1.Text = Whois.serverResponse;//We get the serever and debug resonse and add them the the box
            textBox2.Text = Whois.debugResponse;
        }
        private void portBox_TextChanged(object sender, EventArgs e)
        {
            port = portBox.Text;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserNameBox_TextChanged(object sender, EventArgs e)
        {
            //Check if box is empty
            if(UserNameBox.Text == "")
            {

            }
            else
            {
                username = UserNameBox.Text;
            }
        }

        private void LocationBox_TextChanged(object sender, EventArgs e)
        {
            if (LocationBox.Text == "")
            {
                location = null;
            }
            else
            {
                location = LocationBox.Text;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pSelection = comboBox1.SelectedItem.ToString();
            //Add the right protocol to input based on user choice
            switch (pSelection.Trim())
            {
                case ("HTTP 0.9"):
                    {
                        protocol = "-h9";
                        break;
                    }
                case ("HTTP 1.0"):
                    {
                        protocol = "-h0";
                        break;
                    }
                case ("HTTP 1.1"):
                    {
                        protocol = "-h1";
                        break;
                    }
                case ("WhoIs"):
                    {
                        protocol = "zna!!@";
                        break;
                    }
                default:
                    {
                        protocol = "whois";
                        break;
                    }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                debug = "-d";
            }
            else
            {
                debug = null;
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if(textBox3.Text == "")
            {
                timeout = "1000";
            }
            else
            {
                timeout = textBox3.Text;
            }
        }




        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
