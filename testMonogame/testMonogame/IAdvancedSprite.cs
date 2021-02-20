using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    public interface IAdvancedSprite : ISprite
    {
        //aldready has basic sprite stuff
        //plays attack animation
        public void AttackAnimation();
        //plays item animation
        public void UseItemAnimation();
        //plays move animation
        public void SetIsMoving(bool movingIn);
        public bool isMoving();
        public void damageFlash();


    }
}
