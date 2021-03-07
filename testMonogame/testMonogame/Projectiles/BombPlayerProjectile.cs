using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class BombPlayerProjectile : IPlayerProjectile, ISprite

    {
        Texture2D texture;
        Rectangle destRect;
        Rectangle sourceRect;
        public int X { get; set; }
        public int Y { get; set; }

        Color color = Color.White;

        int explosionInc = 30;
       
        int fuse = 300;
        int countdown;

        public BombPlayerProjectile(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;



            //sourceRect
            //directions, up, down, right, left
            sourceRect = new Rectangle(0, 17, 8, 16);


            


        }

        public void delete(Game1 game)
        {
            game.RemovePlayerProjectile(this);
        }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void doDamage(IEnemy target)
        {
            target.takeDamage(1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            destRect = new Rectangle(X, Y, sourceRect.Width * 2, sourceRect.Height * 2);
            spriteBatch.Draw(texture, destRect, sourceRect, color);




        }

        public void Move()
        {
           //No moving
        }


        public void Update(Game1 game)
        {
            Move();
            //TEMP collision stuff
            countdown++;
            if (countdown>fuse)
            {
                //Boom
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)X , (float)Y)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)X + explosionInc, (float)Y + explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)X , (float)Y + explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)X - explosionInc, (float)Y + explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)X - explosionInc, (float)Y )));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)X - explosionInc, (float)Y - explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)X , (float)Y - explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)X+ explosionInc, (float)Y - explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)X + explosionInc, (float)Y)));

                delete(game);
            }
        }
    }
}
