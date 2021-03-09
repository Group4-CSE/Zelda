using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class DownMovingPlayerState : IPlayerState
    {
        IPlayer player;
        IAdvancedSprite sprite;

        Texture2D projectiles;

        int x;
        int y;

        int vProjectileOffset = 0;
        int hProjectileOffset = 6;

        bool stasis;

        int yVel = 1;
        public DownMovingPlayerState(Texture2D inTexture, Vector2 position, IPlayer inPlayer, Texture2D inProjectiles)
        {
            player = inPlayer;

            projectiles = inProjectiles;

            x = (int)position.X;
            y = (int)position.Y;

            stasis = false;

            sprite = new DownMovingPlayerSprite(inTexture, this);
        }
        public Rectangle getDestRect()
        {
            return sprite.getDestRect();
        }
        public void Attack()
        {
            if (!stasis) sprite.AttackAnimation();

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
            if (!stasis) player.Move(0, yVel);
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
            if (!stasis) game.AddPlayerProjectile(new BombPlayerProjectile(projectiles, new Vector2((float)(player.X),
                (float)(player.Y + 20))));
        }

        public void spawnArrow(Game1 game)
        {
            if (!stasis) game.AddPlayerProjectile(new ArrowPlayerProjectile(projectiles, new Vector2((float)(player.X + hProjectileOffset),
                (float)(player.Y + vProjectileOffset)), new Vector2(0, 5), 1));
        }

        public void spawnBoomerang(Game1 game)
        {
            if (!stasis) game.AddPlayerProjectile(new BoomerangPlayerProjectile(projectiles, new Vector2((float)(player.X + hProjectileOffset),
                (float)(player.Y + vProjectileOffset)), new Vector2(0, 3), 6));
        }

        public void spawnSwordProjectile(Game1 game)
        {
            if (!stasis) game.AddPlayerProjectile(new SwordPlayerProjectile(projectiles, new Vector2((float)(player.X + hProjectileOffset),
                  (float)(player.Y + vProjectileOffset)), new Vector2(0, 3), 1));
        }
        public void damage()
        {
            if (!stasis) sprite.damageFlash();
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
