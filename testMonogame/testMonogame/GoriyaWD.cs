using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    class GoriyaWD : IEnemy, ISprite
    {
        Texture2D texture;
        Rectangle destRect;
        int x;
        int y;
        int health;

        int frame = 1;
        const int width = 13;
        const int height = 16;
        Rectangle sourceRect = new Rectangle(0, 0, 13, 16);
        Color color = Color.White;

        Rectangle frame1 = new Rectangle(0, 0, 13, 16);
        Rectangle frame2 = new Rectangle(15, 0, 13, 16);

        public GoriyaWD(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            x = (int)position.X;
            y = (int)position.Y;
            health = 3;
            destRect = new Rectangle(x, y, width, height);
        }
        public void Attack(IPlayer player)
        {
            // Attack player here if collides
            // Also add attack animation here
            // Boomerang as well
        }

        public void Move()
        {
            y += 1;
        }

        public void takeDamage(int dmg)
        {
            health -= dmg;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destRect = new Rectangle(x, y, width, height);
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
            // Attack();
            // takeDamage();
        }
    }
}
