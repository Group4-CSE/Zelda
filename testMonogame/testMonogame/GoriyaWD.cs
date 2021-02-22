using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    class GoriyaWD : IGoriyaState, ISprite
    {
        Texture2D texture;
        Texture2D projTexture;
        Rectangle destRect;
        GoriyaEnemy goriya;

        int frame = 1;
        const int width = 13;
        const int height = 16;
        Rectangle sourceRect = new Rectangle(0, 0, 13, 16);
        Color color = Color.White;

        Rectangle frame1 = new Rectangle(0, 0, 13, 16);
        Rectangle frame2 = new Rectangle(15, 0, 13, 16);

        public GoriyaWD(Texture2D inText, Texture2D projText, GoriyaEnemy inGoriya)
        {
            texture = inText;
            projTexture = projText;
            goriya = inGoriya;
            destRect = new Rectangle(goriya.getX(), goriya.getY(), width, height);
        }
        public void Attack(IPlayer player)
        {
            goriya.Attack(player);
        }

        public void Move()
        {
            goriya.Move(0, 1);
        }

        public void takeDamage(int dmg)
        {
            goriya.takeDamage(dmg);
        }

        public void spawnBoomerang(Game1 game)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destRect = new Rectangle(goriya.getX(), goriya.getY(), width, height);
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
