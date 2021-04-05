using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame.Interfaces
{
    public interface IDoor
    {

        public void openDoor();

        public void Interact(IPlayer player);

        public Boolean getIsClosed();

        public int getSide();

        public int getNextRoom();
    }
}
