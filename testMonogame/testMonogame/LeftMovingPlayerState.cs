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

            sprite = new LeftMovingPlayerSprite(inTexture, this);
        }

        public void Attack()
        {
            if (!stasis) sprite.AttackAnimation();
            
        }

        public int getX()
        {
            return player.getX();
        }
        public int getY()
        {
            return player.getY();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);

        }

        public void Move()
        {
            if (!stasis) player.Move(xVel, 0);
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

        public void spawnBomb(Game1 game)
        {
            if (!stasis) game.AddPlayerProjectile((ISprite)new BombPlayerProjectile(projectiles, new Vector2((float)(player.getX()-20),
                (float)(player.getY() ))));
        }

        public void spawnArrow(Game1 game)
        {
            if (!stasis) game.AddPlayerProjectile((ISprite)new ArrowPlayerProjectile(projectiles, new Vector2((float)(player.getX()+hProjectileOffset), 
                (float)(player.getY()+vProjectileOffset)), new Vector2(-5,0),3));
        }

        public void spawnBoomerang(Game1 game)
        {
            if (!stasis) game.AddPlayerProjectile((ISprite)new BoomerangPlayerProjectile(projectiles, new Vector2((float)(player.getX()+hProjectileOffset), 
                (float)(player.getY()+vProjectileOffset)), new Vector2(-3,0),4));
        }

        public void spawnSwordProjectile(Game1 game)
        {
            if(!stasis)game.AddPlayerProjectile((ISprite)new SwordPlayerProjectile(projectiles, new Vector2((float)(player.getX() + hProjectileOffset),
                (float)(player.getY() + vProjectileOffset)), new Vector2(-3, 0), 3));
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
        public void Update(Game1 game)
        {
            sprite.Update(game);
        }
    }
}
