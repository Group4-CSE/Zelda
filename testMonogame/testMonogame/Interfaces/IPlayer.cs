using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace testMonogame
{
    public interface IPlayer
    {
        //Had to Set this here
        public int X { get; set; }
        public int Y { get; set; }

        //sets location of player object
        public void SetLocation(Vector2 location);

        //change player state (0=left, 1=up, 2= right, 3=down)
        public void ChangeState(int direction);

        //returns player HP
        public int GetHP();

        //passes in item to player
        public void ObtainItem(String item);

        //damage player
        public void TakeDamage(int damage);
        //deals damage to enemy
        public void dealDamage(int damage);

        //use key
        public Boolean UseKey(int keyType);
        //use bomb if player has one.
        public void UseBomb(Game1 game);
        //use boomerang if the player has it
        public void UseBoomerang(Game1 game);
        //ise bow if possible
        public void UseBow(Game1 game);
        //Draw
        public void Draw(SpriteBatch spriteBatch);
        //Update
        public void Update(Game1 game);
        //Move
        public void Move(int xChange, int yChange);
        //attack
        public void Attack(Game1 game);
        public int getX();
        public int getY();
        public IPlayerState getState();
        //returns the destination rectangle of the sprite associated with this entity
        public Rectangle getDestRect();
        public void SetDamageFrames(int frames);
        public int GetDamageFrames();
    }
}
