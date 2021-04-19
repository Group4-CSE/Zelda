using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testMonogame
{
    public class TrapEnemy : IEnemy, ISprite
    {
        Texture2D texture;
        Rectangle destRect;
        Rectangle sourceRect;
        const int width = 32;
        const int height = 32;
        Color color = Color.White;

        Rectangle triggerRect;
        int xVel;
        int yVel;
        bool isMoving;
        bool returning;
        int cooldown;
        Rectangle verticalLine;
        Rectangle horizontalLine;

        public int X { get; set; }
        public int Y { get; set; }
        int startX;
        int startY;
        int updateFrame;
        int curFrame;
        int enemyVel = 3;
        IPlayer player;
        Rectangle playerDestRect;
        int maxX = 96;
        int maxY = 56;
        int playerOffset = 16;
        int trapOffset = 32;
        bool isInLine = false;

        enum TrapMovement {isStill, isMoving, isReturning};
        TrapMovement movementState = TrapMovement.isStill;
   
        public TrapEnemy(Texture2D eTexture, Vector2 position)
        {
            //Enemy sprite drawing
            texture = eTexture;
            X = (int)position.X;
            startX = (int)position.X;
            Y = (int)position.Y;
            startY = (int)position.Y;
            destRect = new Rectangle(X, Y, width, height);
            verticalLine = new Rectangle(X, 0, width, 1000);
            horizontalLine = new Rectangle(0, Y, 1000, height);


            triggerRect = new Rectangle();
            xVel = 0;
            yVel = 0;
            isMoving = false;
            returning= false;
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Move()
        {
            X += xVel;
            Y += yVel;

            if (!returning && destRect.Intersects(triggerRect))
            {
                //Debug.WriteLine("bounce");
                xVel *= -1;
                yVel *= -1;
                returning = true;
            }
        }

        public void setVelocity()
        {
            Rectangle playerRect = player.getDestRect();
            triggerRect = new Rectangle(playerRect.X+width/2,playerRect.Y+height/2,playerRect.Width-width/2,playerRect.Width-height/2);
            returning = false;

            //top or bottom
            if (playerRect.Intersects(horizontalLine))
            {
                //player is to the right
                if (playerRect.X > startX + width)
                {
                    //Debug.WriteLine("Trap moving right");
                    xVel = enemyVel;
                }
                //left
                else if (playerRect.X < startX) {
                   // Debug.WriteLine("Trap moving left");
                    xVel = -1*enemyVel;
                }
            }

            //right or left
            else if (playerRect.Intersects(verticalLine))
            {
                //player is below
                if (playerRect.Y > startY + height)
                {
                    //Debug.WriteLine("Trap moving down");
                    yVel = enemyVel;
                }
                //above
                else if (playerRect.Y < startY)
                {
                    //Debug.WriteLine("Trap moving up");
                    yVel = -1 * enemyVel;
                }
            }

        }

        // Okay so this lets us avoid calling our massive move function 60 times a second
        // Which by the way is extremely laggy. We need to check if we are "in line" with a trap before we even move
        // This basically makes "Move" a routine determining function and "moveRoutine" the actual move function
        // This function is basically going to exploit the fact that each trap is in a different quadrant, and work in pairs of 2
        public bool inLine()
        {
            Rectangle playerRect = player.getDestRect();
            Rectangle trigger = new Rectangle(playerRect.X + width / 2, playerRect.Y + height / 2, playerRect.Width - width / 2, playerRect.Width - height / 2);


            bool isInLine = trigger.Intersects(verticalLine) || trigger.Intersects(horizontalLine);


            return isInLine;
        }

        public void Attack(IPlayer player)
        {
            player.TakeDamage(4);
        }

        public void takeDamage(int dmg)
        {
           //cant take damage
        }
        public int getHealth() { return 1; }
        public void Draw(SpriteBatch spriteBatch)
        {
            destRect = new Rectangle(X, Y, width, height);
            sourceRect = new Rectangle(0, 0, 16, 16);
            spriteBatch.Draw(texture, destRect, sourceRect, color);
            //spriteBatch.Draw(texture, triggerRect, sourceRect, Color.Orange);
            //spriteBatch.Draw(texture, horizontalLine, sourceRect, Color.Red);
            //spriteBatch.Draw(texture, verticalLine, sourceRect, Color.Red);
            //spriteBatch.Draw(texture, playerDestRect, sourceRect, Color.Orange);

        }

        public void Update(GameManager game)
        {
            player = game.getPlayer();
            playerDestRect = player.getDestRect();
            //Debug.WriteLine(xVel);
            //Debug.WriteLine(yVel);
            if (cooldown == 0)
            {
                isInLine = inLine();
                if (!isMoving && isInLine)
                {
                    isMoving = true;
                    setVelocity();

                }

                if (isMoving)
                {
                    Move();
                    if (returning && (X == startX && Y == startY))
                    {
                        isMoving = false;
                        xVel = 0;
                        yVel = 0;
                        returning = false;
                        cooldown = 60;
                        //triggerRect = new Rectangle();
                    }

                }
            }
            else
            {
                //Debug.WriteLine(cooldown);
                cooldown--;
            }
            
            


        }
    }

}


