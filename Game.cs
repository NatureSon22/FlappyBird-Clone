using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FlappyBirdClone
{
    public partial class Game : Form
    {
        private int pipeSpeed = 5;
        private int gravity = 0;
        private int score = 0;
        private int prevScore = 0;
        


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public Game()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;

            if (FlappyBird.Bounds.Y >= 0)
            {
                FlappyBird.Top += gravity;
            } else FlappyBird.Top = 0;
            BlockBottom.Left -= pipeSpeed;
            BlockTop.Left -= pipeSpeed;
            BlockBottom2.Left -= pipeSpeed;
            BlockTop2.Left -= pipeSpeed;
            ScoreLabel.Text = "Score: " + score;

            if (BlockTop.Left < -70)
            {
                BlockTop.Left = 500;
                score++;
            }
            if (BlockBottom.Left < -80)
            {
                BlockBottom.Left = 750;
                score++;
            }
            if(BlockTop2.Left < -90)
            {
           
                BlockTop2.Left = 900; 
                score++;
            }
            if(BlockBottom2.Left < -100)
            {

                BlockBottom2.Left = 950;
                score++;
            }

            if (FlappyBird.Bounds.IntersectsWith(BlockTop.Bounds) ||
                FlappyBird.Bounds.IntersectsWith(BlockTop2.Bounds) ||
                FlappyBird.Bounds.IntersectsWith(BlockBottom.Bounds) ||
                FlappyBird.Bounds.IntersectsWith(BlockBottom2.Bounds) ||
                FlappyBird.Bounds.IntersectsWith(Ground.Bounds)
                )
            {
                endGame(timer, score);
            }

            if(score == prevScore + 10)
            {
                pipeSpeed += 5;
                prevScore = score;
            }
        }
        private void KeyDownPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -8;
            }

        }

        private void KeyUpPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 12;
            }
        }

        private void endGame(Timer timer, int score)
        {
            timer.Stop();
            GameOver game = new GameOver(score, this);
            this.Opacity = 0.8;
            game.Show();
        }
     }
}
