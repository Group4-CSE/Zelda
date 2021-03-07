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
        int randX;
        int randY;
        int updateFrame;
        int curFrame;
        int health;
        Random randomNumber = new Random();
        int enemyVel = 3;
        int modifier;

        //Set these constraints for left and right movement (Consider up and down later)
        int maxX = 600;
        int minX = 300;
        bool mHorizontal = false;
        int maxFrame = 2;
   
        public TrapEnemy(Texture2D eTexture, Vector2 position)
        {
            //Enemy sprite drawing
            texture = eTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            destRect = new Rectangle(X, Y, width, height);

            //Enemy movement
            updateFrame = randomNumber.Next(200);
            curFrame = 0;
            randX = randomNumber.Next(enemyVel);
            randY = randomNumber.Next(enemyVel);
            
            //Enemy state
            health = 100;
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Move()
        {
            //Will Change this move later, for sprint 2 moves left and right
            updateFrame++;
            if (updateFrame == 10)
            {
                updateFrame = 0;
                curFrame++;

                if (curFrame == maxFrame)
                {
                    curFrame = 0;
                }
                //Changed this to move left and right
                if (X == maxX)
                {
                    mHorizontal = true;

                }
                else if (X == minX)
                {
                    mHorizontal = false;
                }

                if (mHorizontal)
                {
                    X -= 50;
                }
                else
                {
                    X += 50;
                }
            }

            //Prevents from going off screen
            if (X + randX < 0 || X + width + randX > 800) randX *= -1;
            X += randX;
        }

        public void Attack(IPlayer player)
        {

        }

        public void takeDamage(int dmg)
        {
            health -= dmg;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destRect = new Rectangle(X, Y, width, height);
            sourceRect = new Rectangle(0, 0, 16, 16);
            spriteBatch.Draw(texture, destRect, sourceRect, color);
            
        }

        public void Update(Game1 game)
        {
            Move();
        }
    }

}


