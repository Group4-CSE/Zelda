using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    public class OpenDoor: ISprite, IObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        int keyType;
        Boolean isClosed;

        Texture2D texture;
        Rectangle destRect;
        Rectangle sourceRect;
        int side;

        int nextRoom;

        public OpenDoor(int direction, Vector2 pos, Texture2D texture, int key, Boolean closed, int next)
        {
            this.texture = texture;
            X = (int)pos.X;
            Y = (int)pos.Y;
            destRect = new Rectangle(X, Y, 65, 65);
            keyType = key;
            isClosed = closed;
            side = direction;
            nextRoom = next;

            switch (direction)
            {
                case 0:     //north door
                    sourceRect = new Rectangle(0, 0, 32, 32);
                    break;
                case 1:     //west door
                    sourceRect = new Rectangle(0, 33, 32, 32);
                    break;
                case 2:     //east door
                    sourceRect = new Rectangle(0, 66, 32, 32);
                    break;
                case 3:     //south door
                    sourceRect = new Rectangle(0, 99, 32, 32);
                    break;
                default:
                    sourceRect = new Rectangle(0, 0, 32, 32);
                    break;

            }

            
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Update(GameManager game)
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

        public void openDoor()
        {
            isClosed = false;
            sourceRect.X = 0;
        }

        public Boolean getIsClosed()
        {
            return isClosed;
        }

        public int getSide()
        {
            return side;
        }

        public int getNextRoom()
        {
            return nextRoom;
        }
    }
}
