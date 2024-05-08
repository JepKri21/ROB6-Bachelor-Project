using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Teeeset_program
{
    public partial class Form1 : Form
    {

        private teeeest _teeeest = new teeeest();

        private connectionHandler _connectionHandler = new connectionHandler();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CONNECTIONSTATUS status = _connectionHandler.ConnectAndGainMastership();
            // lbl_ConnectDisplay.Text = status.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _teeeest.moving_left();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _teeeest.get_position();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _teeeest.moving_right();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _teeeest.rotation_around_own_axis();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _teeeest.full_motion();
        }
    }
}