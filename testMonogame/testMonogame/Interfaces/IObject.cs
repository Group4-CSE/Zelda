using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    public interface IObject
    {
       
        //Draw
        public void Draw(SpriteBatch spriteBatch);
        //Update
        public void Update(GameManager game);
        public int X { get; set; }
        public int Y { get; set; }
  


        /*this function handles whatever happens when the player touches the object. 
         * whether that be collision, pushing, or picking it up.
         * This may end up turning into a private method depending on
         * how we use it, but since every object will have some rule for interaction
         * we might as well have it hear for now at least.
         */
        public void Interact(IPlayer player);
        //returns the destination rectangle of the sprite associated with this entity
        public Rectangle getDestRect();


    }
}
