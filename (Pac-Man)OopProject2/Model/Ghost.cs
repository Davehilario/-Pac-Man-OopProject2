using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;


namespace _Pac_Man_OopProject2.Model
{
    public class Ghost
    {
        int normalSpeed = 8;
        int chaseSpeed = 4;
        int modeTimer = 0;
        Random random = new Random();
        string[] randomDirections = { "left", "right", "up", "down" };
        string direction = "left";

        public PictureBox image = new PictureBox();
        private Form game;
        int minX, minY, maxX, maxY;

        
        public Ghost(Form game, Image img, int x, int y)
        {
            this.game = game;
            image.Image = img;
            image.SizeMode = PictureBoxSizeMode.StretchImage;
            image.Width = 40;
            image.Height = 40;
            image.Left = x;
            image.Top = y;

            game.Controls.Add(image);

            // Define boundaries based on the form's client area.
            minX = 0;
            minY = 0;
            maxX = game.ClientSize.Width;
            maxY = game.ClientSize.Height;
        }

        public void GhostMovement(PictureBox pacman)
        {
            // Save the current position so we can revert if a wall collision happens.
            int oldLeft = image.Left;
            int oldTop = image.Top;

            // Decide on a new mode/direction when the timer expires.
            if (modeTimer <= 0)
            {
                modeTimer = random.Next(50, 80);
                // 50% chance to chase (seek); otherwise, choose a random wandering direction.
                if (random.NextDouble() < 0.5)
                    direction = "seek";
                else
                    direction = randomDirections[random.Next(randomDirections.Length)];
            }
            else
            {
                modeTimer--;
            }

            // Move the ghost based on the selected mode/direction.
            switch (direction)
            {
                case "left":
                    image.Left -= normalSpeed;
                    break;
                case "right":
                    image.Left += normalSpeed;
                    break;
                case "up":
                    image.Top -= normalSpeed;
                    break;
                case "down":
                    image.Top += normalSpeed;
                    break;
                case "seek":
                    // Chase Pac-Man by adjusting position on both axes.
                    if (image.Left > pacman.Left)
                        image.Left -= chaseSpeed;
                    if (image.Left < pacman.Left)
                        image.Left += chaseSpeed;
                    if (image.Top > pacman.Top)
                        image.Top -= chaseSpeed;
                    if (image.Top < pacman.Top)
                        image.Top += chaseSpeed;
                    break;
                default:
                    break;
            }

            // Check collisions with walls. This loops through all controls that are marked as "wall".
            bool collision = false;
            foreach (Control ctrl in game.Controls)
            {
                if (ctrl is PictureBox && ctrl.Tag != null && ctrl.Tag.ToString() == "wall")
                {
                    if (image.Bounds.IntersectsWith(ctrl.Bounds))
                    {
                        collision = true;
                        break;
                    }
                }
            }
            // If a collision with a wall is detected, revert to the previous position and reset the mode timer.
            if (collision)
            {
                image.Left = oldLeft;
                image.Top = oldTop;
                modeTimer = 0; // Force a mode change next tick.
                // If chasing and colliding, optionally switch to a random direction.
                if (direction == "seek")
                    direction = randomDirections[random.Next(randomDirections.Length)];
            }

            // Clamp the ghost within the form boundaries.
            if (image.Left < minX)
                image.Left = minX;
            if (image.Top < minY)
                image.Top = minY;
            if (image.Left + image.Width > maxX)
                image.Left = maxX - image.Width;
            if (image.Top + image.Height > maxY)
                image.Top = maxY - image.Height;
        }

    }
}
