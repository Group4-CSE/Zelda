using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    public class LockedDoor : ISprite, IObject
    {
        int x;
        int y;
        int keyType;
        Boolean isLocked;

        Texture2D texture;
        Rectangle destRect;
        Rectangle sourceRect;

        public LockedDoor(int direction, Vector2 pos, Texture2D texture, int key, Boolean locked)
        {
            this.texture = texture;
            x = (int)pos.X;
            y = (int)pos.Y;
            destRect = new Rectangle(x, y, 33, 33);
            keyType = key;
            isLocked = locked;

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
                unlockDoor();
            }
        }

        public void Update(Game1 game)
        {
            //collision
        }

        public void Interact(IPlayer player)
        {
            if (isLocked)
            {
                if (player.useKey(keyType))
                {
                    unlockDoor();
                }
            }
            else
            {
                // add level changing logic
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, sourceRect, Color.Green);
        }

        public void unlockDoor()
        {
            isLocked = false;
            sourceRect.X = sourceRect.X - 33;
        }
    }
}
