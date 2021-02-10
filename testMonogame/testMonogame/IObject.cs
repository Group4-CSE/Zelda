using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    public interface IObject
    {
        //Something will typically implement both an object and a sprite. so this interface doesnt handle drawing.


        public void Update(Game1 game);


        /*this function handles whatever happens when the player touches the object. 
         * whether that be collision, pushing, or picking it up.
         * This may end up turning into a private method depending on
         * how we use it, but since every object will have some rule for interaction
         * we might as well have it hear for now at least.
         */
        public void Interact();
        //TODO: Add IPlayer player as a parameter once the player interface is implemented.

    }
}
