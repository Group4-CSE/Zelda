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
        int x;
        int y;
        int xVel;
        int yVel;

        Color color = Color.White;



        Rectangle sourceRect;
        public ArrowPlayerProjectile(Texture2D inTexture, Vector2 position, Vector2 velocity, int direction)
        {
            texture = inTexture;
            x = (int)position.X;
            y = (int)position.Y;
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

        public void delete(Game1 game)
        {
            game.RemovePlayerProjectile(this);
        }

        public void doDamage(IEnemy target)
        {
            target.takeDamage(1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            destRect = new Rectangle(x, y, sourceRect.Width*2, sourceRect.Height*2);
            spriteBatch.Draw(texture, destRect, sourceRect, color);




        }

        public void Move()
        {
            x += xVel;
            y += yVel;
        }


        public void Update(Game1 game)
        {
            Move();
            //TEMP collision stuff
            if (x < 0 || x > 800 || y < 0 || y > 480)
            {
                delete(game);
            }
        }
    }
}
