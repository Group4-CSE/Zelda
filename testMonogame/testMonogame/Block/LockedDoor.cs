using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using testMonogame.Interfaces;

namespace testMonogame
{
    public class LockedDoor : ISprite, IObject, IDoor, IBlock
    {
        public int X { get; set; }
        public int Y { get; set; }
        int keyType;
        Boolean isLocked;

        Texture2D texture;
        Rectangle destRect;
        Rectangle orig;
        Rectangle sourceRect;
        int side;

        int nextRoom;

        public LockedDoor(int direction, Vector2 pos, Texture2D texture, int key, Boolean locked,int next)
        {
            this.texture = texture;
            X = (int)pos.X;
            Y = (int)pos.Y;
            destRect = new Rectangle(X, Y, 65, 65);
            orig = new Rectangle(X, Y, 65, 65);
            keyType = key;
            isLocked = locked;
            side = direction;
            nextRoom = next;

            switch (direction)
            {
                case 0:     //north door
                    sourceRect = new Rectangle(33, 0, 32, 32);
                    break;
                case 1:     //west door
                    sourceRect = new Rectangle(33, 33, 32, 32);
                    break;
                case 2:     //east door
                    sourceRect = new Rectangle(33, 66, 32, 32);
                    break;
                case 3:     //south door
                    sourceRect = new Rectangle(33, 99, 32, 32);
                    break;
                default:
                    sourceRect = new Rectangle(33, 0, 32, 32);
                    break;

            }

            if (!isLocked)
            {
                openDoor();
            }
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Update(GameManager game)
        {
            //door, no need for interaction
        }

        public void Interact(IPlayer player)
        {
            if (isLocked)
            {
                if (player.UseKey(keyType))
                {
                    openDoor();
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, sourceRect, Color.White);
        }

        public void openDoor()
        {
            isLocked = false;
            sourceRect.X = 0;
        }

        public Boolean getIsClosed()
        {
            return isLocked;
        }

        public int getSide()
        {
            return side;
        }

        public int getNextRoom()
        {
            return nextRoom;
        }

        public void transitionShift(int x, int y)
        {
            destRect.X = destRect.X + x;
            destRect.Y = destRect.Y + y;
        }
        public void resetToOriginalPos()
        {
            destRect.X = orig.X;
            destRect.Y = orig.Y;
        }
    }
}
