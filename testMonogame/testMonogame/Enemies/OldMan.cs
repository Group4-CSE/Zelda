using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class OldMan : IEnemy, ISprite
    {

        //drawing stuff
        Texture2D texture;
        Rectangle sourceRect = new Rectangle(0, 0, 16, 16);
        Rectangle destRect;
        Color color = Color.White;


        //location stuff
        public int X { get; set; }
        public int Y { get; set; }
        const int width = 32;
        const int height = 32;


        public OldMan(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;

            //Non-moving block so instantiate dest rectangle
            destRect = new Rectangle(X, Y, width, height);

        }
        public int getHealth() { return 1; }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Attack(IPlayer player)
        {
            //Nothing to put here
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, sourceRect, color);

        }

        public void Move()
        {
           //Nothing to put here
        }

        public void takeDamage(int dmg)
        {
           //Nothing to put
        }

        public void Update(GameManager game)
        {
            //Nothing to put here
        }
    }
}
