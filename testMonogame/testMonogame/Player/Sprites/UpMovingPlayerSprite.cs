using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class UpMovingPlayerSprite : IAdvancedSprite
    {
        Texture2D texture;
        Rectangle destRect;
        Color color = Color.White;

        IPlayerState state;

        int damaged;

        int frameCounter;
        int frameDelay = 10;


        LinkedList<Rectangle> frames = new LinkedList<Rectangle>();
        LinkedListNode<Rectangle> currentFrame;

        bool moving;
        public UpMovingPlayerSprite(Texture2D inTexture, IPlayerState inState)
        {
            texture = inTexture;
            state = inState;



            frames.AddLast(new Rectangle(68, 0, 16, 16));
            frames.AddLast(new Rectangle(85, 0, 16, 16));

            currentFrame = frames.First;

            damaged = 0;

            moving = false;

        }
        public Rectangle getDestRect()
        {
            return currentFrame.Value;
        }
        public void AttackAnimation()
        {
            frames.AddFirst(new Rectangle(51, 54, 16, 18));
            frames.AddFirst(new Rectangle(34, 46, 16, 26));
            frames.AddFirst(new Rectangle(17, 45, 16, 27));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            frameCounter++;
            if (frameCounter > frameDelay)
            {
                if (frames.Count > 2)
                {
                    currentFrame = frames.First;
                    frames.RemoveFirst();
                }
                else if (moving)
                {
                    if (currentFrame.Next == null) currentFrame = frames.First;
                    else currentFrame = currentFrame.Next;

                }
                else currentFrame = frames.First;
                frameCounter = 0;
                if (damaged > 0) damaged--;
            }

            destRect = new Rectangle(state.getX() - (currentFrame.Value.Width - 16) * 2, state.getY() - (currentFrame.Value.Height - 16) * 2, currentFrame.Value.Width * 2, currentFrame.Value.Height * 2);

            spriteBatch.Draw(texture, destRect, currentFrame.Value, color);
        }

        public void SetIsMoving(bool movingIn)
        {
            moving = movingIn;
        }

        public void Update(Game1 game)
        {
            if (damaged > 0) state.setStasis(true);
            else state.setStasis(false);
        }

        public void UseItemAnimation()
        {
            frames.AddFirst(new Rectangle(102, 17, 16, 16));
        }
        public bool isMoving()
        {
            bool ret;
            if (frames.Count < 3) ret = moving;
            else ret = false;
            return ret;
        }
        public void damageFlash()
        {
            damaged = 8;
            state.setStasis(true);
            frames.AddFirst(new Rectangle(170, 51, 16, 16));
            frames.AddFirst(new Rectangle(187, 51, 16, 16));
            frames.AddFirst(new Rectangle(170, 51, 16, 16));
            frames.AddFirst(new Rectangle(187, 51, 16, 16));
            frames.AddFirst(new Rectangle(170, 51, 16, 16));
            frames.AddFirst(new Rectangle(187, 51, 16, 16));
            frames.AddFirst(new Rectangle(170, 51, 16, 16));
            frames.AddFirst(new Rectangle(187, 51, 16, 16));


        }
    }
}
