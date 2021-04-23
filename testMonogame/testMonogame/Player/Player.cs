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
        public bool invincible { get; set; }
        
        public int health {  get; set; }
        
        public int maxHealth { get; set ; }
        //how long the attack lasts
        int AttackTimer=30;
        int AttackCount;
        int delay = 0;
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

            ObtainItem("Arrow");
            InitializeFromConstants();//initialize until changed
            


            SelectItem(1);

            invincible = false;
            maxHealth = 12;
            health = maxHealth;
            sound1 = sounds;
        }
        public void InitializeFromConstants()
        {
            maxHealth = GameplayConstants.STARTING_HEALTH;
            health = maxHealth;
            Rupees = GameplayConstants.PLAYER_STARTING_RUPEES;
            if(GameplayConstants.PLAYER_STARTING_BOMBS>0)ObtainItem("Bomb");
            Bombs = GameplayConstants.PLAYER_STARTING_BOMBS;

        }
        public string GetSelectedItem() { return selectedItem; }
        public void SelectItem(int i) { if(!inventory[i].Equals("Arrow"))selectedItem = inventory[i];
        }
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
            enemy.takeDamage((int)(2.0*GameplayConstants.PLAYER_DEAL_DAMAGE_MODIFIER));
            
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
            X += xChange * (int)GameplayConstants.PLAYER_SPEED_MODIFIER ;
            Y += yChange* (int)GameplayConstants.PLAYER_SPEED_MODIFIER;
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
                    sound1.getStuff(0);
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
                    sound1.getStuff(0);
                    break;
                case "Bomb":
                    Bombs = Bombs + 1;
                    //sound1.getstuff(0);
                    //Error due to the call in constructor, cannot make sound before game
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

        public void fireSpin(GameManager game)
        {
            Vector2 pos = new Vector2(X + state.getDestRect().Width / 2, Y + state.getDestRect().Height / 2);
            game.AddPlayerProjectile(new FireBallPlayerProjectile(projectiles, pos, new Vector2(-1, 0)));
            game.AddPlayerProjectile(new FireBallPlayerProjectile(projectiles, pos, new Vector2(-1, -1)));
            game.AddPlayerProjectile(new FireBallPlayerProjectile(projectiles, pos, new Vector2(0, -1)));
            game.AddPlayerProjectile(new FireBallPlayerProjectile(projectiles, pos, new Vector2(1, -1)));
            game.AddPlayerProjectile(new FireBallPlayerProjectile(projectiles, pos, new Vector2(1, 0)));
            game.AddPlayerProjectile(new FireBallPlayerProjectile(projectiles, pos, new Vector2(1,1)));
            game.AddPlayerProjectile(new FireBallPlayerProjectile(projectiles, pos, new Vector2(0, 1)));
            game.AddPlayerProjectile(new FireBallPlayerProjectile(projectiles, pos, new Vector2(-1, 1)));


        }

        public void SetLocation(Vector2 location)
        {
            X = (int)location.X;
            Y = (int)location.Y;
        }

        public void TakeDamage(int damage)
        {
            if (!state.getStasis() && !invincible)
            {
                int damageDealt = (int)(damage * GameplayConstants.PLAYER_TAKE_DAMAGE_MODIFIER);
                if (damageDealt < 1) damageDealt = 1;//make sure no damage is reduced to 0
                health -= damageDealt;
                sound1.Link_Hurt();
                state.damage();
            }
        }
        public void Update(GameManager game)
        {
            //int delay = 0;
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
            //Low Health Sounds
            if (health <= 2)
            {
                //Delay to keep sound as beep rather than eeeeee
               
                delay++;

                if(delay == 20)
                {
                    sound1.lowHP();
                    delay -= 20;
                }

            }
            if (health <= 0)
            {

                sound1.pDies();
                game.SetState(3);
            }
            
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
                sound1.Door();
                Keys--;
                return true;
               
            }


            


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

        public void PlaceRupeeShield(GameManager game)
        {
            Rupees = Rupees - 1;
            state.PlaceRupeeShield(game);
        }
        //instead of using a bow and arrow link uses his own health to send an arrow forth sapping him for 1/2 a heart, this will be healed back if the arrow hits
        public void UseReapingArrow(GameManager game)
        {
            if (health > 2)
            {
                health = health - 2;
                state.spawnReapingArrow(game);
            }
        }
    }
}
