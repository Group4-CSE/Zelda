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
            // Boundary checking, +16 represents Link's Height so we're not just checking the top left corner
            // This only allows us to know if player is in the line of sight, but not which direction
            // So we need player dest rect to compare to in each case as well
            if (player.X + 16 > X || player.X < X + width)
            {
                // If the player is below us
                if (playerDestRect.Y < Y && Y > maxY)
                {
                    Y += enemyVel;
                } 
                else if (playerDestRect.Y > Y && Y < maxY) // Player is above us
                {
                    Y -= enemyVel;
                } 
                else // We need to wind back our trap
                {
                    // enemyVel / 2 since the traps wind back slower than they go forward
                    while (X > startX)
                    {
                        X -= enemyVel / 2;
                    }
                    while (X < startX)
                    {
                        X += enemyVel / 2;
                    }
                    while (Y > startY)
                    {
                        Y -= enemyVel / 2;
                    }
                    while (Y < startY)
                    {
                        Y -= enemyVel / 2;
                    }
                }
            } else if (player.Y < Y + height || player.Y + 16 > Y)
            {
                // If the player is to the left
                if (playerDestRect.X < X && X > maxX)
                {
                    X -= enemyVel;
                } 
                else if (playerDestRect.X > X && X < maxX) // Player is to the right
                {
                    X += enemyVel;
                }
                else
                {
                    // enemyVel / 2 since the traps wind back slower than they go forward
                    while (X > startX)
                    {
                        X -= enemyVel / 2;
                    }
                    while (X < startX)
                    {
                        X += enemyVel / 2;
                    }
                    while (Y > startY)
                    {
                        Y -= enemyVel / 2;
                    }
                    while (Y < startY)
                    {
                        Y -= enemyVel / 2;
                    }
                }
            }
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

        public void Update(Game1 game)
        {
            player = game.getPlayer();
            playerDestRect = player.getDestRect();
            Move();
        }
    }

}


