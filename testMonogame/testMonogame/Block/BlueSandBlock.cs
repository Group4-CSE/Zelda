using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
     class BlueSandBlock: IObject, ISprite
    {
        //Initialize
        Texture2D texture;
        Rectangle destRect;
        Rectangle sourceRect = new Rectangle(17, 17, 16, 16);
        Color color = Color.White;

        //Might need to change these cordinates depending on where to place.
        public int X { get; set; }
        public int Y { get; set; }
        Vector2 location;
        const int width = 32;
        const int height = 32;

        public BlueSandBlock(Texture2D BlockTexture, Vector2 pos)
        {
            texture = BlockTexture;
            X = (int)pos.X;
            Y = (int)pos.Y;

            destRect = new Rectangle(X, Y, width, height);

        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Update(GameManager game)
        {
            //N/A
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, sourceRect, color);
        }

        public void Interact(IPlayer player)
        {
            throw new NotImplementedException();
            //Should be Null right now
        }
    }
}
