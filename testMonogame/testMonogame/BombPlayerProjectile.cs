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
        int x;
        int y;


        Color color = Color.White;

        int explosionInc = 30;
       
        int fuse = 300;
        int countdown;

        public BombPlayerProjectile(Texture2D inTexture, Vector2 position)
        {
            texture = inTexture;
            x = (int)position.X;
            y = (int)position.Y;



            //sourceRect
            //directions, up, down, right, left
            sourceRect = new Rectangle(0, 17, 8, 16);


            


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

            destRect = new Rectangle(x, y, sourceRect.Width * 2, sourceRect.Height * 2);
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
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)x , (float)y)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)x + explosionInc, (float)y + explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)x , (float)y + explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)x - explosionInc, (float)y + explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)x - explosionInc, (float)y )));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)x - explosionInc, (float)y - explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)x , (float)y - explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)x+ explosionInc, (float)y - explosionInc)));
                game.AddPlayerProjectile(new ExplosionPlayerProjectile(texture, new Vector2((float)x + explosionInc, (float)y)));

                delete(game);
            }
        }
    }
}
