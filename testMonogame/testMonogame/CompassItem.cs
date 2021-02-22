using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class CompassItem : IObject, ISprite
    {
        //drawing stuff
        Texture2D texture;
        Rectangle sourceRect = new Rectangle(11, 0, 11, 12);
        Rectangle destRect;
        Color color = Color.White;


        //location stuff
        int x;
        int y;
        const int width = 22;
        const int height = 24;


        public CompassItem(Texture2D inTexture, Vector2 position)
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
            //TODO: Add Code to guide player to tri force shard. only relevant once we get a map in place
        }

        public void Update(Game1 game)
        {
            //Nothing Needed here
        }
    }
}
