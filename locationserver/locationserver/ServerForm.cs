using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace locationserver
{
    public partial class ServerForm : Form
    {
        int readTform = 1000;
        int writeTform = 1000;
        public ServerForm()
        {
            InitializeComponent();
            ReadTimeoutBox1.Text = "1000";
            WriteTimeoutBox2.Text = "1000";
            StartServer.FlatStyle = FlatStyle.Flat;
            StartServer.FlatAppearance.BorderSize = 0;
        }

        private void StartServer_Click(object sender, EventArgs e)
        {
            new Thread(Program.runServer).Start();
            label3.Text = "ONLINE";
            label3.ForeColor = Color.Lime;
            Program.readT = readTform;
            Program.writeT = writeTform;
        }

        private void ReadTimeoutBox1_TextChanged(object sender, EventArgs e)
        {
            if (ReadTimeoutBox1.Text == "")
            {
                readTform = 1000;
            }
            else
            {
                readTform = int.Parse(ReadTimeoutBox1.Text);
            }
        }

        private void WriteTimeoutBox2_TextChanged(object sender, EventArgs e)
        {
            writeTform = int.Parse(WriteTimeoutBox2.Text);
        }
    }
}
