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
        Rectangle sourceRect = new Rectangle(18, 18, 34, 16);
        Color color = Color.Green;

        //Might need to change these cordinates depending on where to place.
        int xPosition;
        int yPosition;
        Vector2 location;
        const int width = 16;
        const int height = 16;

        public BlueSandBlock(Texture2D BlockTexture, Vector2 pos)
        {
            texture = BlockTexture;
            xPosition = (int)pos.X;
            yPosition = (int)pos.Y;

            destRect = new Rectangle(xPosition, yPosition, width, height);

        }

        public void Update(Game1 game)
        {
            throw new NotImplementedException();
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
