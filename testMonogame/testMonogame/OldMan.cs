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
        int x;
        int y;
        const int width = 16;
        const int height = 16;


        public OldMan(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            x = (int)position.X;
            y = (int)position.Y;

            //Non-moving block so instantiate dest rectangle
            destRect = new Rectangle(x, y, width, height);

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

        public void Update(Game1 game)
        {
            //Nothing to put here
        }
    }
}
