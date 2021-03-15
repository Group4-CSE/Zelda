using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class LeftMovingPlayerState : IPlayerState
    {
        IPlayer player;
        IAdvancedSprite sprite;

        Texture2D projectiles;

        int x;
        int y;
        int vProjectileOffset = 8;
        int hProjectileOffset = 0;

        bool stasis;

        int xVel = -1;
        public LeftMovingPlayerState(Texture2D inTexture, Vector2 position,IPlayer inPlayer, Texture2D inProjectiles)
        {
            player = inPlayer;

            projectiles = inProjectiles;

            x = (int)position.X;
            y = (int)position.Y;

            stasis = false;

            sprite = new LeftMovingPlayerSprite(inTexture, this, player.GetDamageFrames());
        }
        public Rectangle getDestRect()
        {
            return sprite.getDestRect();
        }
        public void Attack()
        {
            if (!stasis) sprite.AttackAnimation();
            
        }
        public void SetDamaged(int framesRemaining)
        {
            player.SetDamageFrames(framesRemaining);
        }
        public int getX()
        {
            return player.X;
        }
        public int getY()
        {
            return player.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);

        }

        public void Move()
        {
           player.Move(xVel, 0);
        }

        public void PlaceItem()
        {
            if (!stasis) sprite.UseItemAnimation();
        }
        public void setMoving(bool moving)
        {
            sprite.SetIsMoving(moving);
        }
        public bool isMoving()
        {
            return sprite.isMoving();
        }

        public void spawnBomb(GameManager game)
        {
            if (!stasis) game.AddPlayerProjectile(new BombPlayerProjectile(projectiles, new Vector2((float)(player.X-20),
                (float)(player.Y ))));
        }

        public void spawnArrow(GameManager game)
        {
            if (!stasis) game.AddPlayerProjectile(new ArrowPlayerProjectile(projectiles, new Vector2((float)(player.X+hProjectileOffset), 
                (float)(player.Y+vProjectileOffset)), new Vector2(-5,0),3));
        }

        public void spawnBoomerang(GameManager game)
        {
            if (!stasis) game.AddPlayerProjectile(new BoomerangPlayerProjectile(projectiles, new Vector2((float)(player.X+hProjectileOffset), 
                (float)(player.Y+vProjectileOffset)), new Vector2(-3,0),4));
        }

        public void spawnSwordProjectile(GameManager game)
        {
            if(!stasis)game.AddPlayerProjectile(new SwordPlayerProjectile(projectiles, new Vector2((float)(player.X + hProjectileOffset),
                (float)(player.Y + vProjectileOffset)), new Vector2(-3, 0), 3));
        }
        public void damage()
        {
            if(!stasis)sprite.damageFlash();
        }
        public void setStasis(bool stasisIn)
        {
            stasis = stasisIn;

        }
        public bool getStasis()
        {
            return stasis;
        }
        public void Update(GameManager game)
        {
            sprite.Update(game);
        }
    }
}
