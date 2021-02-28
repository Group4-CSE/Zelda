using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class TriforceItem : IObject, ISprite
    {
        //drawing stuff
        Texture2D texture;
        Rectangle sourceRect = new Rectangle(0, 0, 10, 10);
        Rectangle destRect;
        Color color = Color.White;
        

        //location stuff
        int x;
        int y;
        const int width = 20;
        const int height = 20;


        public TriforceItem(Texture2D inTexture, Vector2 position)
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
            //TODO: Add code for picking up triforce (probably just ends game since that is when u finish dungeon)
        }

        public void Update(Game1 game)
        {
            //Nothing Needed here
        }
    }
}
