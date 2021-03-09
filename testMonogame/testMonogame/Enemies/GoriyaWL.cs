using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    class GoriyaWL : IGoriyaState, ISprite
    {
        Texture2D texture;
        Texture2D projTexture;
        Rectangle destRect;
        GoriyaEnemy goriya;

        int frame = 1;
        const int width = 26;
        const int height = 32;
        Rectangle sourceRect = new Rectangle(0, 33, 13, 16);
        Color color = Color.White;
        float hProjectileOffset = 0;
        float vProjectileOffset = 0;

        Rectangle frame1 = new Rectangle(0, 33, 13, 16);
        Rectangle frame2 = new Rectangle(14, 33, 13, 16);

        public GoriyaWL(Texture2D inText, Texture2D projText, GoriyaEnemy inGoriya)
        {
            texture = inText;
            projTexture = projText;
            goriya = inGoriya;
            destRect = new Rectangle(goriya.X, goriya.Y, width, height);
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Attack(IPlayer player)
        {
            goriya.Attack(player);
        }

        public void Move()
        {
            goriya.Move(-1, 0);
        }

        public void takeDamage(int dmg)
        {
            goriya.takeDamage(dmg);
        }

        public void spawnBoomerang(Game1 game)
        {
            goriya.setThrow(true);
            game.AddEnemyProjectile((IEnemyProjectile)new BoomerangEnemyProjectile(projTexture, new Vector2((float)(goriya.X + hProjectileOffset),
                (float)(goriya.Y + vProjectileOffset)), new Vector2(-3, 0), 2, goriya));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destRect = new Rectangle(goriya.X, goriya.Y, width, height);
            frame += 1;
            if (frame > 60) frame = 0;
            if (goriya.getThrow() == true)
            {
                sourceRect = frame2;
            }
            else
            {
                if (frame < 30)
                {
                    sourceRect = frame1;
                }
                else
                {
                    sourceRect = frame2;
                }
            }

            spriteBatch.Draw(texture, destRect, sourceRect, color);
        }

        public void Update(Game1 game)
        {
            if (goriya.getThrow() == false)
            {
                Move();
            }
            // Attack();
            // takeDamage();
        }
    }
}
