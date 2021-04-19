using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    class PushableBlock : IObject, ISprite, IBlock
    {

        Texture2D texture;
        Rectangle destRect;
        Rectangle orig;
        String pushSide;
        bool pushed;
        public int X { get; set; }
        public int Y { get; set; }

        Rectangle sourceRect = new Rectangle(17, 0, 16, 16);
        Color color = Color.White;

        public PushableBlock(Texture2D incTexture, Vector2 pos,String pushSideIn)
        {
            texture = incTexture;
            X = (int)pos.X;
            Y = (int)pos.Y;
            destRect = new Rectangle(X, Y, 32, 32);
            orig = new Rectangle(X, Y, 32, 32);
            pushed = false;
            pushSide = pushSideIn;
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Update(GameManager game)
        {
            // NULL
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destRect = new Rectangle(X, Y, 32, 32);
            spriteBatch.Draw(texture, destRect, sourceRect, color);
        }

        public void Interact(IPlayer player)
        {
            if (pushed == false)
            {
                Rectangle playerRect = player.getDestRect();
                Rectangle intersect = Rectangle.Intersect(destRect, playerRect);
                //this will only be called if we already know that they are intersecting.
                int xCollisionSize = intersect.Width;
                int yCollisionSize = intersect.Height;

                if (xCollisionSize > yCollisionSize)
                {
                    //top
                    if (playerRect.Y < destRect.Y)
                    {
                        if (pushSide.Equals("top"))
                        {
                            Y = Y + destRect.Height;
                            orig.Y = Y;
                            pushed = true;
                        }
                        }
                    else // bottom
                    {
                        if (pushSide.Equals("bottom"))
                        {
                            Y = Y - destRect.Height;
                            orig.Y = Y;
                            pushed = true;
                        }
                    }
                }
                else // We are in a Left / Right style collision
                {
                    // left
                    if (playerRect.X < destRect.X)
                    {
                        if (pushSide.Equals("left"))
                        {
                            X = X + destRect.Width;
                            orig.X = X;
                            pushed = true;
                        }
                    }
                    else //right
                    {
                        if (pushSide.Equals("right"))
                        {
                            X = X - destRect.Width;
                            orig.X = X;
                            pushed = true;
                        }
                        }
                }
            }
            
        }

        public void transitionShift(int x, int y)
        {
            //destRect.X = destRect.X + x;
            //destRect.Y = destRect.Y + y;
            X = X + x;
            Y = Y + y;
        }
        public void resetToOriginalPos()
        {
            X = orig.X;
            Y = orig.Y;
        }
    }
}
