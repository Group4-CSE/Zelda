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
        int maxHealth=12;
        //how long the attack lasts
        int AttackTimer=30;
        int AttackCount;
        int Rupees;

        List<String> inventory= new List<string>();

         IPlayerState state;
        Texture2D projectiles;
        bool attack;
        Sounds sound1;
        public Player(Texture2D inTexture, Vector2 position, Texture2D inProjectiles, Sounds sounds)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            Rupees = 0;
            attack = false;
            projectiles = inProjectiles;
            state = new LeftMovingPlayerState(texture, new Vector2(X, Y),this, inProjectiles);
            AttackCount = 0;

            inventory.Add("Bomb");
            inventory.Add("Bow");
            inventory.Add("Arrow");
            inventory.Add("Boomerang");

            health = maxHealth;
            sound1 = sounds;
        }
        public bool IsAttacking() { return attack; }
        public int GetDirection()
        {
            //0=up, 1=right, 2=down, 3= left
            int direction = 0;
            if (state is RightMovingPlayerState) direction = 1;
            else if (state is DownMovingPlayerState) direction = 2;
            else if (state is LeftMovingPlayerState) direction = 3;
            return direction;
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

        public void Attack(GameManager game)
        {
            if (!state.getStasis())
            {
                state.Attack();
                sound1.Sword_Slash();
                attack = true;
                if (health == maxHealth)
                {
                    sound1.Sword_Shoot();
                    state.spawnSwordProjectile(game);
                }
            }
        }

        public void ChangeState(int direction)
        {
            if(direction==0) state= new UpMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
            else if (direction == 1) state = new DownMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
            else if (direction == 2) state = new RightMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
            else if (direction == 3) state = new LeftMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
        }

        public void dealDamage(IEnemy enemy)
        {
            sound1.EnemyHitDie(0);
            enemy.takeDamage(1);
            
            //Figure out sound for enemy dying
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
            switch (item)
            {
                case "Clock":
                    //freeze screen
                    break;
                case "Compass":
                    //guide to triforce
                    break;
                case "Fiary":
                    health = maxHealth;
                    break;
                case "Heart":
                    health += 4;
                    sound1.getStuff(1);
                    if (health > maxHealth) health = maxHealth;
                    break;
                case "Map":
                    //display rooms on map
                    break;
                case "PermanentHeart":
                    maxHealth += 4;
                    health += 4;
                    break;
                case "Rupee":
                    Rupees++;
                    sound1.getStuff(2);
                    break;
                default:
                    inventory.Add(item);
                    sound1.getStuff(0);
                    break;
            }
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
                sound1.Link_Hurt();
                state.damage();
            }
        }
        public void Update(GameManager game)
        {
            if (state.isMoving()) state.Move();
            state.Update(game);

            if (attack == true)
            {
                AttackCount++;
                if (AttackCount > AttackTimer)
                {
                    attack = false;
                    AttackCount = 0;
                }
            }
        }

        public void UseBomb(GameManager game)
        {
            if (inventory.Contains("Bomb"))
            {
                state.PlaceItem();
                sound1.BombD(0);
                state.spawnBomb(game, sound1);
                //sound1.BombD(1);
                //player looses one bomb later
            }
            
            
        }

        public void UseBoomerang(GameManager game)
        {
            if (inventory.Contains("Boomerang"))
            {
                sound1.Arr_Boom();
                state.PlaceItem();
                state.spawnBoomerang(game);
                inventory.Remove("Boomerang");
            }
        }
        
        public void UseBow(GameManager game)
        {
            if (inventory.Contains("Bow") && inventory.Contains("Arrow"))
            {
                sound1.Arr_Boom();
                state.PlaceItem();
                state.spawnArrow(game);
                //player looses one arrow, later
            }
        }
        
        public bool UseKey(int keyType)
        {
            //Add logic for opening doors later
            //sound1.Door()
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
