using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace testMonogame
{
    public interface IPlayer
    {
        //sets location of player object
        public void setLocation(Vector2 location);

        //sets direction for movement or still player
        // 0 = north 2 = east 3 = south 4 = west --> this can be changed  
        public void setDirection(int direction);

        //returns player HP
        public float getHP();

        //passes in item to player
        public void getItem(IObject item);

        //damage player
        public void takeDamage(float damage);
    }
}
