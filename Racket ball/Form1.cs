using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Racket_ball
{
    public partial class Form1 : Form
    {
        // global variables 
        Rectangle player1 = new Rectangle(10, 130, 10, 60);
        Rectangle player2 = new Rectangle(10, 200, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);
        //Rectangle outline = new Rectangle(10, 130, 11, 61);

        int player1Score = 0;
        int player2Score = 0;
        int playerTurn = 1;

        int playerSpeed = 4;
        int ballXSpeed = -6;
        int ballYSpeed = 6;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool aDown = false;
        bool dDown = false;
        bool leftDown = false;
        bool rightDown = false;

        Pen outlinePen = new Pen(Color.White, 2);
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        public Form1()
        {
            InitializeComponent();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            // move player
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }
            if (aDown == true && player1.X >0)
            {
                player1.X -= playerSpeed;
            }
            if (dDown == true && player1.X < this.Height - player1.Height )
            {
                player1.X += playerSpeed;
            }
           
            
            //move player 2 
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }
            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }
            if (leftDown == true && player2.X >0 )
            {
                player2.X -= playerSpeed;
            }
            if (rightDown == true && player2.X < this.Height - player2.Height)
            {
                player2.X += playerSpeed;
            }

            //check if ball hit top or bottom wall and change direction if it does 
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            }

            //ball collison with player
            if (player1.IntersectsWith(ball) && playerTurn %2 ==0 )
            {
                ballXSpeed *= -1;
                ball.X = player1.X + ball.Width;
            }
            else if (player2.IntersectsWith(ball) && playerTurn %2 != 0)
            {
                ballXSpeed *= -1;
                ball.X = player2.X + ball.Width;
            }
            //check for point scored
            //check if a player missed the ball and if true add 1 to score of other player  
            if (ball.X < 0 & playerTurn % 2 != 0)
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                ball.X = 295;
                ball.Y = 195;

                player1.Y = 130;
                player2.Y = 200;
            }
            else if (ball.X < 0 & playerTurn % 2 == 0)
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";
                player1.Y = 130;
                player2.Y = 200;

                ballXSpeed *= -1;
                playerTurn++;
            }
            else if (ball.X > 600)
            {
                ballXSpeed *= -1;
                playerTurn++;
            }

                //check for game over
                // check score and stop game if either player is at 3 
                if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";
            }
            Refresh();


        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
           
           if (playerTurn % 2 == 0)
            {
                e.Graphics.DrawRectangle(outlinePen, player1);
            }
           else
            {
                e.Graphics.DrawRectangle(outlinePen, player2);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
               



            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.A:
                    aDown = true; 
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
            }
        }
    }
}
