using System;
using System.Collections.Generic;
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
            

        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Move()
        {
            /*
             * Movement Logic:
             * - All traps are independent of each other, and need to act as such
             * - We are a trap enemy, we must check the player's rectange and see if he is in our line of sight,
             * to do that we need to see if he is on the same X / Y value of us exclusively and not inclusively
             * - To determine if he is above/below us, we must check the absolute y value and compare it to ours
             * - To determine if he is left/right of us, we must check the absolute x value and compare it to ours
             * - We will go through a routine instead of active updating, since active updating is what bugged us
             * - REMEMBER: All calculations routed at top left corner, must add offset for proper bounds
             * - Any condition with an offset will refer to a right or bottom bound
             * 
             * Direction Guide:
             * 1: Up
             * 2: Down
             * 3: Left
             * 4: Right
             */

            // Lets do up and down first
            // If the player is under us on either the left or right respectively
            // "Is the player's top left X smaller than our top right X? Or is the player's top right X greater than our top left X?"
            if (playerDestRect.X < startX + trapOffset || playerDestRect.X + playerOffset > startX)
            {
                // Okay so now we need to move either up or down, so let's figure out which one
                // "Is the player's Y greater than (under) ours?" If so, we move down
                if (playerDestRect.Y > startY)
                {
                    moveRoutine(2);
                } 
                // If not, than our player's Y is less than (above ours) and we move up
                else if (playerDestRect.Y < startY)
                {
                    moveRoutine(1);
                }
            } 
            // Okay now we have to do left or right
            // "Is the player's top left Y smaller than our bottom left? Or is the player's bottom left greater than our top left?"
            else if (playerDestRect.Y < Y + trapOffset || playerDestRect.Y + playerOffset > Y)
            {
                // Okay so now we need to move either left or right, so let's figure out which one
                // "Is the player's X greater than (right) ours?" If so we move right
                if (playerDestRect.X > startX)
                {
                    moveRoutine(4);
                }
                // If not, than our player's X is less than (left) and we move left
                else if (playerDestRect.X < startX)
                {
                    moveRoutine(3);
                }
            }
        }

        /*
         * TODO: Finish moveRoutine, possible bug in inLine or other line of sight detection
         * THIS IS STILL BROKEN IDK WHY
         * 
         * Direction Guide:
         * 1: Up
         * 2: Down
         * 3: Left
         * 4: Right
         */
        public void moveRoutine(int direction)
        {
            // Leaving the starting position
            if (movementState == TrapMovement.isMoving)
            {
                if (direction == 1)
                {
                    Y -= enemyVel;
                    if (Y <= maxY)
                    {
                        Y = maxY;
                        movementState = TrapMovement.isReturning;
                    }
                }
                if (direction == 2)
                {
                    Y += enemyVel;
                }
                if (direction == 3)
                {
                    X -= enemyVel;
                }
                if (direction == 4)
                {
                    X += enemyVel;
                }
            }
            // Returning to starting position
            else if (movementState == TrapMovement.isReturning)
            {
                if (direction == 1)
                {
                    Y += enemyVel;
                    if (Y >= startY)
                    {
                        Y = startY;
                        movementState = TrapMovement.isStill;
                    }
                }
            }
          
        }

        // Okay so this lets us avoid calling our massive move function 60 times a second
        // Which by the way is extremely laggy. We need to check if we are "in line" with a trap before we even move
        // This basically makes "Move" a routine determining function and "moveRoutine" the actual move function
        // This function is basically going to exploit the fact that each trap is in a different quadrant, and work in pairs of 2
        public bool inLine()
        {
            bool isInLine = false;

            // This will check if we are in line with the LEFT SIDE traps for going UP & DOWN
            // "Is the player's top left X less than our top right X AND is the player coming from the right side of us?"
            // This works because the player will NEVER be greater than the RIGHT SIDE trap's starting X (i.e. farther right)
            if (playerDestRect.X < X + trapOffset && playerDestRect.X > startX)
            {
                isInLine = true;
            }
            // RIGHT SIDE traps for going UP & DOWN
            // "Is the player's top right X greater than our top left X AND is the player coming from the left side of us?"
            else if (playerDestRect.X + playerOffset > X && playerDestRect.X < startX + trapOffset)
            {
                isInLine = true;
            }
            // TOP SIDE traps for going LEFT & RIGHT
            // "Is the player's top left Y smaller than our bottom left Y AND is the player coming from the bottom side of us?"
            else if (playerDestRect.Y < Y + trapOffset && playerDestRect.Y > startY)
            {
                isInLine = true;
            }
            // BOTTOM SIDE traps for going LEFT & RIGHT
            // "Is the player's bottom left Y greater than our top left Y AND is the player coming from the top side of us?"
            else if (playerDestRect.Y + playerOffset > Y && playerDestRect.Y > Y + trapOffset)
            {
                isInLine = true;
            }

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
            
        }

        public void Update(GameManager game)
        {
            player = game.getPlayer();
            playerDestRect = player.getDestRect();
            isInLine = inLine();
            if (isInLine)
            {
                movementState = TrapMovement.isMoving;
                Move();
            }
        }
    }

}


