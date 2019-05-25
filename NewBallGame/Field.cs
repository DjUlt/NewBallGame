using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Timers;

namespace NewBallGame
{
    public partial class Field : Form
    {
        public Field()
        {
            InitializeComponent();
           
            t.Tick += new EventHandler(TimerT);
            t.Interval = 1000;
            v.Tick += new EventHandler(TimerV);
            v.Interval = 200;
            field1 = new GameField(10, 10);
            field1.CreateField();
            t.Start();
            v.Start();
            startgame = false;
        }

        Timer t = new Timer();
        Timer v = new Timer();

        static public bool endgame = false;
        static public bool startgame = false;
        static public bool mainmenu = true;
        public static int selector = 1;
        public static int totaltime = 0;
        public static int clearedfields = 0;
        public static string output = "";

        static GameField field1;

        private void Field_Load(object sender, EventArgs e)
        {
            
            


        }

        void GameEndField()
        {
            field1.ClearField();
            
            t.Stop();
            v.Stop();
            label1.Text = "";
            label1.Text="\n\n\n                      Game Over"+"                          :(\n"+"                time spent(m.s.ms): " + (int)(totaltime / 60000) + "." + (int)((totaltime / 1000) % 60) + "." + totaltime % 1000 + "\n"+"                orbs collected: " + field1.Orbs + "\n"+"                total points: " + (int)(field1.Orbs * 233 + 60000 * clearedfields / Math.Sqrt(totaltime)) + "\n\n"+ "                    press ESC to exit the game\n                      R to quit to main menu";
            clearedfields = 0;
        }

        void TimerV(object sender, EventArgs e)
        {
            //visualize
            label1.Text = "";
            field1.time += 200;

            label1.Text=field1.winformvisualize()+"\nOrbs collected: " + field1.Orbs+"\nTime spent(m.s.ms): " + (int)(field1.time / 60000) + "." + (int)((field1.time / 1000) % 60) + "." + field1.time % 1000+"\nTime left(m.s.ms): " + (int)((60000 * field1.X / 2 - field1.time) / 60000) + "." + (int)(((60000 * field1.X / 2 - field1.time) / 1000) % 60) + "." + (60000 * field1.X / 2 - field1.time) % 1000+"\nControls: \nZ - /, X - \\, C - clear,\nR - to quit to main menu.\nUse arrows to move cursor.\nPress ESC to exit.";
            if (60000 * field1.X / 2 - field1.time < 1) { totaltime += field1.time; GameEndField(); }
            GC.Collect();
        }

        void TimerT(object sender, EventArgs e)
        {
            //Case of all orbs absorbed
            if (field1.IsCleared())
            {
                clearedfields++;
                totaltime += field1.time;
                field1.ClearField();
                field1.CreateField();
            }

            if (field1.ball1.NextTrap(field1))
            {
                GameEndField();
            }

            //moveball
            field1.ball1.Move(field1);

            //Forced garbage collect
            GC.Collect();
        }

        void Field_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                field1.selector.MoveDown();
            }
            else if (e.KeyData == Keys.Down)
            {
                field1.selector.MoveUp();
            }
            else if (e.KeyData == Keys.Right)
            {
                field1.selector.MoveRight();
            }
            else if (e.KeyData == Keys.Left)
            {
                field1.selector.MoveLeft();
            }
            else if (e.KeyData == Keys.Z)
            {
                field1.selector.SetS(field1);
            }
            else if (e.KeyData == Keys.X)
            {
                field1.selector.SetBS(field1);
            }
            else if (e.KeyData == Keys.C)
            {
                field1.selector.SetC(field1);
            }
            else if(e.KeyData == Keys.Escape)
            {
                Environment.Exit(0);
            }
            else if (e.KeyData == Keys.R)
            {
                t.Stop();
                v.Stop();
                t.Dispose();
                v.Dispose();
                field1.ClearField();
                this.Close();
            }
        }
    }
}
