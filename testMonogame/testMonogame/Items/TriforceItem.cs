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
        public int X { get; set; }
        public int Y { get; set; }
        const int width = 20;
        const int height = 20;


        public TriforceItem(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;

            //Non-moving block so instantiate dest rectangle
            destRect = new Rectangle(X, Y, width, height);

        }
        public Rectangle getDestRect()
        {
            return destRect;
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
