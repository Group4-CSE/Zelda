using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class ReapingArrowPlayerProjectile : IPlayerProjectile, ISprite

    {
        Texture2D texture;
        Rectangle destRect;
        public int X { get; set; }
        public int Y { get; set; }
        int xVel;
        int yVel;

        Color color = Color.White;

        IPlayer p;

        Rectangle sourceRect;
        public ReapingArrowPlayerProjectile(Texture2D inTexture, Vector2 position, Vector2 velocity, int direction,IPlayer inP)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            xVel = (int)velocity.X;
            yVel = (int)velocity.Y;

            p = inP;
            //sourceRect
            //directions, up, down, right, left
            Rectangle[] directionalArrow = { new Rectangle(25, 119, 7, 16),
                                             new Rectangle(33, 120, 7, 16),
                                             new Rectangle(41, 120, 16, 7),
                                             new Rectangle(40, 128, 16, 7)};
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

        public void doDamage(IEnemy target, Sounds sounds)
        {
            sounds.EnemyHitDie(0);
            target.takeDamage(1);
            p.health += 2;
            if (p.health > p.maxHealth) p.health = p.maxHealth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            destRect = new Rectangle(X, Y, sourceRect.Width * 2, sourceRect.Height * 2);
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
