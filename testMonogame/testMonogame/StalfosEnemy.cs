using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testMonogame
{
    class StalfosEnemy : IEnemy, ISprite
    {
        Texture2D texture;
        Rectangle destRect;
        int x;
        int y;
        int health;
        int xRand;
        int yRand;
        int modifier;

        int enemyVel = 3;
        Random randomNumber = new Random();
        int frame = 1;
        const int width = 16;
        const int height = 16;
        Rectangle sourceRect = new Rectangle(70, 0, 86, 16);
        Color color = Color.White;

        Rectangle frame1 = new Rectangle(70, 0, 86, 16);
        Rectangle frame2 = new Rectangle(87, 0, 103, 16);

        public StalfosEnemy(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            x = (int)position.X;
            y = (int)position.Y;
            destRect = new Rectangle(width, height, x, y);
            health = 100;
        }

        public void Move()
        {
            // Code for making sure the stalfos is on a solid block and can move will be here
            // TEMP: No walk off screen
            if (x > 0 && x < 800 && y > 0 && y < 480)
            {
                xRand = randomNumber.Next(enemyVel);
                yRand = randomNumber.Next(enemyVel);
                modifier = randomNumber.Next(enemyVel);
                xRand -= modifier;
                yRand -= modifier;

                if (x + xRand < 0 || x + xRand > 800) xRand *= -1;
                if (y + yRand < 0 || y + yRand > 480) yRand *= -1;
                x += xRand;
                y += yRand;
            }
        }

        public void Attack(IPlayer player)
        {
            // If this enemy collides with the player, set frame and do damage
        }

        public void takeDamage(int dmg)
        {
            health -= dmg;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            frame += 1;
            if (frame > 60) frame = 0;
            if (frame < 30)
            {
                sourceRect = frame1;
            }
            else if (frame > 30 && frame < 60)
            {
                sourceRect = frame2;
            }

            spriteBatch.Draw(texture, destRect, sourceRect, color);
        }

        public void Update(Game1 game)
        {
            Move();
            // If (enemy collides with player) attack(player)
            // If (playerAttack || playerProjectile collides with self) takeDamage(dmg)
        }

    }
}
