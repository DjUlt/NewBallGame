using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NewBallGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(@"Records.txt", Encoding.UTF8);
            //FileStream stream = new FileStream(@"Records.txt", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            //TextReader reader = new StreamReader(stream);
            textBox1.Lines = lines;
            //reader.Close();
            
        }

    }
}
