using System;
namespace testMonogame
{
    public interface IBlock
    {
        public void transitionShift(int x, int y);
        public void resetToOriginalPos();
    }
}
