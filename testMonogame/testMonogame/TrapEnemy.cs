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
        Rectangle sourceRect = new Rectangle(0, 0, 16, 16);
        const int width = 16;
        const int height = 16;
        Color color = Color.White;

        int x;
        int y;
        int randX;
        int randY;
        int directionFrame;
        int directionCounter;
        int health;
        Random randomNumber = new Random();
        int enemyVel = 3;
        int modifier;
        int frame = 1;

        Rectangle frame1 = new Rectangle(0, 0, 16, 16);

        public TrapEnemy(Texture2D eTexture, Vector2 position)
        {
            //Enemy sprite drawing
            texture = eTexture;
            x = (int)position.X;
            y = (int)position.Y;
            destRect = new Rectangle(x, y, width, height);

            //Enemy movement
            directionFrame = randomNumber.Next(200);
            directionCounter = 0;
            randX = randomNumber.Next(enemyVel);
            randY = randomNumber.Next(enemyVel);
            modifier = randomNumber.Next(enemyVel);
            randX -= modifier;
            randY -= modifier;
            int frame = 1;

            //Enemy state
            health = 100;
        }

        public void Move()
        {

            if (directionCounter > directionFrame)
            {
                randX = randomNumber.Next(enemyVel);
                randY = randomNumber.Next(enemyVel);
                modifier = randomNumber.Next(enemyVel);
                //Might change this cuz trap goes towards another trap?
                randX -= modifier;
                randY -= modifier;
                directionFrame = randomNumber.Next(200);
                directionCounter = 0;

            }
            else
            {
                directionCounter += 1;
            }

            //Prevents from going off screen
            if (x + randX < 0 || x + width + randX > 800) randX *= -1;
            if (y + randY < 0 || y + height + randY > 480) randY *= -1;
            x += randX;
            y += randY;

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
            destRect = new Rectangle(x, y, width, height);
            frame += 1;
            if (frame > 60)
            {
                frame = 0;
            }
            //No need to do frame since theres one sprite for animation
            sourceRect = frame1;

            spriteBatch.Draw(texture, destRect, sourceRect, color);

        }

        public void Update(Game1 game)
        {
            Move();
        }
    }

}


