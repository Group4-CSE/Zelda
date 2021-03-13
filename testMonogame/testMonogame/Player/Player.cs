using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class Player : IPlayer
    {
        Texture2D texture;
        public int X { get; set; }
        public int Y { get; set; }
        int damageFrames;
        int health;
        int maxHealth=10;

        List<String> inventory= new List<string>();

         IPlayerState state;
        Texture2D projectiles;

        public Player(Texture2D inTexture, Vector2 position, Texture2D inProjectiles)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;

            projectiles = inProjectiles;
            state = new LeftMovingPlayerState(texture, new Vector2(X, Y),this, inProjectiles);


            inventory.Add("Bomb");
            inventory.Add("Bow");
            inventory.Add("Arrow");
            inventory.Add("Boomerang");

            health = maxHealth;
        }
        public void SetDamageFrames(int frames)
        {
            damageFrames = frames;
        }
        public int GetDamageFrames() { return damageFrames; }
       public Rectangle getDestRect()
        {
            return state.getDestRect();
        }

        public void Attack(Game1 game)
        {
            state.Attack();
            if (health == maxHealth)state.spawnSwordProjectile(game) ;
        }

        public void ChangeState(int direction)
        {
            if(direction==0) state= new UpMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
            else if (direction == 1) state = new DownMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
            else if (direction == 2) state = new RightMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
            else if (direction == 3) state = new LeftMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
        }

        public void dealDamage(int damage)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            state.Draw(spriteBatch);
        }

        public int GetHP()
        {
            return health;
        }

        public void Move(int xChange, int yChange)
        {
            X += xChange;
            Y += yChange;
        }

        public void ObtainItem(String item)
        {
            inventory.Add(item);
        }

        public void SetLocation(Vector2 location)
        {
            X = (int)location.X;
            Y = (int)location.Y;
        }

        public void TakeDamage(int damage)
        {
            if (!state.getStasis())
            {
                health -= damage;
                state.damage();
            }
        }
        public void Update(Game1 game)
        {
            if (state.isMoving()) state.Move();
            state.Update(game);
        }

        public void UseBomb(Game1 game)
        {
            if (inventory.Contains("Bomb"))
            {
                state.PlaceItem();
                state.spawnBomb(game);
                //player looses one bomb later
            }
            
            
        }

        public void UseBoomerang(Game1 game)
        {
            if (inventory.Contains("Boomerang"))
            {
                state.PlaceItem();
                state.spawnBoomerang(game);
                inventory.Remove("Boomerang");
            }
        }
        
        public void UseBow(Game1 game)
        {
            if (inventory.Contains("Bow") && inventory.Contains("Arrow"))
            {
                state.PlaceItem();
                state.spawnArrow(game);
                //player looses one arrow, later
            }
        }
        
        public bool UseKey(int keyType)
        {
            //Add logic for opening doors later
            return false;
        }

        public IPlayerState getState()
        {
            return state;
        }

        public int getX()
        {
            return X;
        }

        public int getY()
        {
            return Y;
        }
    }
}
