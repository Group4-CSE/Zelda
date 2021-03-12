using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame
{
    public class CaveDoor : ISprite, IObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        Texture2D texture;
        Rectangle destRect;
        Rectangle sourceRect;

        public CaveDoor(int direction, Vector2 pos, Texture2D texture)
        {
            this.texture = texture;
            X = (int)pos.X;
            Y = (int)pos.Y;
            destRect = new Rectangle(X, Y, 65, 65);

            switch (direction)
            {
                case 0:     //north door
                    sourceRect = new Rectangle(99, 0, 32, 32);
                    break;
                case 1:     //west door
                    sourceRect = new Rectangle(99, 33, 32, 32);
                    break;
                case 2:     //east door
                    sourceRect = new Rectangle(99, 66, 32, 32);
                    break;
                case 3:     //south door
                    sourceRect = new Rectangle(99, 99, 32, 32);
                    break;
                default:
                    sourceRect = new Rectangle(99, 0, 32, 32);
                    break;

            }
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Update(Game1 game)
        {
            //collision
        }

        public void Interact(IPlayer player)
        {
            // add level changing logic
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, sourceRect, Color.White);
        }
    }
}
