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

        const int hitboxShrink = 2;
        
        //int arrowCount;
        public int Rupees { get; set; }
        public int Keys { get; set; }
        public int Bombs { get; set; }
        public bool Compass { get; set; }
        public bool Map { get; set; }

        List<String> inventory= new List<string>();
         IPlayerState state;
        Texture2D projectiles;

        string selectedItem;

        bool attack;
        Sounds sound1;
        public Player(Texture2D inTexture, Vector2 position, Texture2D inProjectiles, Sounds sounds)
        {
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            Rupees = 25;
            attack = false;
            projectiles = inProjectiles;
            state = new LeftMovingPlayerState(texture, new Vector2(X, Y),this, inProjectiles);
            AttackCount = 0;
            Map = false;
            Compass = false;

            ObtainItem("Bomb");
            ObtainItem("Bomb");
            ObtainItem("Arrow");

            SelectItem(0);

            health = maxHealth;
            sound1 = sounds;
        }
        public string GetSelectedItem() { return selectedItem; }
        public void SelectItem(int i) { if(!inventory[i].Equals("Arrow"))selectedItem = inventory[i]; }
        public void NextItem() {
            int i = inventory.IndexOf(selectedItem) + 1;
            if (i > inventory.Count - 1) i = 0;
            SelectItem(i);
        }


        public void PreviousItem()
        {
            int i = inventory.IndexOf(selectedItem) - 1;
            if (i <0) i = inventory.IndexOf(selectedItem);
            SelectItem(i);
        }
        
        public List<String> GetInventory()
        {
            return inventory;
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
            Rectangle r= state.getDestRect();
            Rectangle dest = r;
            if(!attack)dest = new Rectangle(r.X+hitboxShrink, r.Y + hitboxShrink, r.Width-hitboxShrink, r.Height-hitboxShrink);
            return dest;
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
            if (direction == 0) state = new UpMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
            else if (direction == 1) state = new DownMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
            else if (direction == 2) state = new RightMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
            else if (direction == 3) state = new LeftMovingPlayerState(texture, new Vector2(X, Y), this, projectiles);
            else if (direction == 5) state= new WinPlayerState(texture, new Vector2(X,Y), this,projectiles);
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
                    //freeze screen later
                    break;
                case "Compass":
                    //guide to triforce
                    Compass = true;
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
                    Map = true;
                    break;
                case "PermanentHeart":
                    maxHealth += 4;
                    health += 4;
                    break;
                case "Rupee":
                    Rupees=Rupees+1;
                    sound1.getStuff(2);

                    break;
                case "Key":
                    Keys = Keys + 1;
                    break;
                case "Bomb":
                    Bombs = Bombs + 1;

                    if(!inventory.Contains(item))inventory.Add(item);
                    break;
                case "Arrow":
                    if(!inventory.Contains(item))inventory.Add(item);
                    break;
                case "Triforce":
                    ChangeState(5);
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
            if (health <= 0) game.SetState(3);
            if (state is WinPlayerState) game.SetState(4);
        }

        public void UseBomb(GameManager game)
        {
            if (inventory.Contains("Bomb"))
            {
                state.PlaceItem();

                sound1.BombD(0);
                state.spawnBomb(game, sound1);
                Bombs = Bombs - 1;
                if (Bombs <= 0)
                {
                    inventory.Remove("Bomb");
                    selectedItem = "";
                }
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
            if (inventory.Contains("Bow") && inventory.Contains("Arrow") && Rupees>0)
            {
                sound1.Arr_Boom();
                state.PlaceItem();
                state.spawnArrow(game);
                Rupees--;
            }
        }
        
        public bool UseKey(int keyType)
        {
            if (Keys > 0)
            {
                Keys--;
                return true;
            }


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
