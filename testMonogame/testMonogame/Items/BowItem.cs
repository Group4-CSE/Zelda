using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class BowItem : IObject, ISprite
    {
        //drawing stuff
        Texture2D texture;
        Rectangle sourceRect = new Rectangle(18, 13, 8, 17);
        Rectangle destRect;
        Color color = Color.White;


        //location stuff
        int x;
        int y;
        const int width = 16;
        const int height = 34;


        public BowItem(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            x = (int)position.X;
            y = (int)position.Y;

            //Non-moving block so instantiate dest rectangle
            destRect = new Rectangle(x, y, width, height);

        }
        public void Draw(SpriteBatch spriteBatch)
        {




            spriteBatch.Draw(texture, destRect, sourceRect, color);

        }

        public void Interact(IPlayer player)
        {
            //TODO: Add code for adding bow to inventory
        }

        public void Update(Game1 game)
        {
            //Nothing Needed here
        }
    }
}
