using System;
namespace testMonogame
{
    public interface IController
    {
        //update controller
        public void Update();
        //figure out what to do w/ input
        public void RegisterCommand(Keys key, ICommand command);
    }
}
