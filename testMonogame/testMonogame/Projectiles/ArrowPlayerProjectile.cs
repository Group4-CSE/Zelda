using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class ArrowPlayerProjectile : IPlayerProjectile, ISprite

    {
        Texture2D texture;
        Rectangle destRect;
        public int X { get; set; }
        public int Y { get; set; }
        int xVel;
        int yVel;

        Color color = Color.White;



        Rectangle sourceRect;
        public ArrowPlayerProjectile(Texture2D inTexture, Vector2 position, Vector2 velocity, int direction)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            xVel = (int)velocity.X;
            yVel = (int)velocity.Y;


            //sourceRect
            //directions, up, down, right, left
            Rectangle[] directionalArrow = { new Rectangle(9, 17, 7, 16),
                                             new Rectangle(17, 17, 7, 16),
                                             new Rectangle(25, 17, 16, 7),
                                             new Rectangle(25, 25, 16, 7)};
            sourceRect = directionalArrow[direction];



        }
        public void collide(GameManager game)
        {
            delete(game);
        }

        public void delete(GameManager game)
        {
            game.RemovePlayerProjectile(this);
        }

        public void doDamage(IEnemy target)
        {
            target.takeDamage(1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            destRect = new Rectangle(X, Y, sourceRect.Width*2, sourceRect.Height*2);
            spriteBatch.Draw(texture, destRect, sourceRect, color);




        }

        public void Move()
        {
            X += xVel;
            Y += yVel;
        }


        public void Update(GameManager game)
        {
            Move();
            //TEMP collision stuff
            if (X < 0 || X > 800 || Y < 0 || Y > 480)
            {
                delete(game);
            }
        }

        public Rectangle getDestRect()
        {
            return destRect;
        }
    }
}
