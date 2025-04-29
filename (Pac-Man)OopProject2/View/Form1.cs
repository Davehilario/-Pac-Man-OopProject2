using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _Pac_Man_OopProject2.Model;
using _Pac_Man_OopProject2.View;

namespace _Pac_Man_OopProject2
{
    public partial class Form1 : Form, Iform1
    {
        bool up, down, left, right;
        bool noUp, noDown,noLeft, noRight;
        List<PictureBox> walls = new List<PictureBox>();
        List<PictureBox> coins = new List<PictureBox>();
        int speed = 12;
        int score = 0;

        Ghost red, yellow, blue, pink;
        private object ghosts;

        public Form1()
        {
            InitializeComponent();
            Setup();
        }

        private void pictureBox38_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox93_Click(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel1.Visible = false;

            left = right = up = down = false;
            noLeft = noRight = noUp = noDown = false;
            score = 0;

            GameTimer.Start();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            PlayerMovements();

            foreach (PictureBox wall in walls)
            {
                CheckBoundaries(pacman, wall);
            }

            foreach (PictureBox coin in coins)
            {
                CollectingCoins(pacman, coin);
            }
            if (score == coins.Count)
            {
                ShowCoins();
                score = 0;
            }

            red.GhostMovement(pacman);
            yellow.GhostMovement(pacman);
            blue.GhostMovement(pacman);
            pink.GhostMovement(pacman);
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && !noLeft)
            {
                right = down = up = false;
                noRight = noDown = noUp = false;
                left = true;
                pacman.Image = Properties.Resources.pacman_left;
            }
            if (e.KeyCode == Keys.Right && !noRight)
            {
                left = up = down = false;
                noLeft = noUp = noDown = false;
                right = true;
                pacman.Image = Properties.Resources.pacman_right;
            }
            if (e.KeyCode == Keys.Up && !noUp)
            {
                left = right = down = false;
                noLeft = noRight = noDown = false;
                up = true;
                pacman.Image = Properties.Resources.pacman_up;
            }
            if (e.KeyCode == Keys.Down && !noDown)
            {
                left = right = up = false;
                noLeft = noRight = noDown = false;
                down = true;
                pacman.Image = Properties.Resources.pacman_down;
            }
        }



        private void Setup()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "wall")
                {
                    walls.Add((PictureBox)x);
                }
                if (x is PictureBox && x.Tag == "coin")
                {
                    coins.Add((PictureBox)x);
                }
            }

            red = new Ghost(this, Properties.Resources.red, 100, 100);
            
            blue = new Ghost(this, Properties.Resources.blue, 100, 400);
            
            yellow = new Ghost(this, Properties.Resources.yellow, 650, 400);
            
            pink = new Ghost(this, Properties.Resources.pink, 650, 100);
            

        }

        private void PlayerMovements()
        {
            if (left) { pacman.Left -= speed; }
            if (right) { pacman.Left += speed; }
            if (up) { pacman.Top -= speed; }
            if (down) { pacman.Top += speed; }

            if (pacman.Left < -30)
            {
                pacman.Left = this.ClientSize.Width - pacman.Width;
            }
            if (pacman.Left + pacman.Width > this.ClientSize.Width)
            {
                pacman.Left = -10;
            }
            if (pacman.Top < -30)
            {
                pacman.Top = this.ClientSize.Height - pacman.Height;
            }
            if (pacman.Top + pacman.Width > this.ClientSize.Height)
            {
                pacman.Top = -10;
            }
        }

        private void ShowCoins()
        {
            foreach (PictureBox coin in coins)
            {
                coin.Visible = true;
            }
        }

        private void CheckBoundaries(PictureBox pacman, PictureBox wall)
        {
            if (pacman.Bounds.IntersectsWith(wall.Bounds))
            {
                if (left)
                {
                    left = false;
                    noLeft = true;
                    pacman.Left = wall.Right + 2;
                }
                if (right)
                {
                    right = false;
                    noRight = true;
                    pacman.Left = wall.Left - pacman.Width - 2;
                }
                if (up)
                {
                    up = false;
                    noUp = true;
                    pacman.Top = wall.Bottom + 2;
                }
                if (down)
                {
                    down = false;
                    noDown = true;
                    pacman.Top = wall.Top - pacman.Height - 2;
                }
            }

        }

        private void CollectingCoins(PictureBox Pacman, PictureBox coin)
        {
            if (pacman.Bounds.IntersectsWith(coin.Bounds))
            {
                if (coin.Visible)
                {
                    coin.Visible = false;
                    score += 1;
                }
            }
        }

        private void Ghost(Ghost g, PictureBox Pacman, PictureBox ghost)
        {

        }

        private void GameOver()
        {

        }

        public void InitializeGameUI()
        {
            
        }

        public void UpdateScore(int score)
        {
            
        }

        public void ShowStartScreen()
        {
            
        }

        public void HideStartScreen()
        {
            
        }
    }
}
