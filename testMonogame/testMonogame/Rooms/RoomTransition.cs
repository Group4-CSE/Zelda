using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using testMonogame.Interfaces;

namespace testMonogame.Rooms
{
    public class RoomTransition
    {
        IRoom current;
        IRoom next;
        int direction;
        int delay;
        Boolean transition;
        public RoomTransition()
        {
            delay = 0;
            transition = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!next.isTransitioning())
            {
                //when the next room is done transitioning reset current room and stop transitioning
                current.setTransitioning(false);
                current.resetToOriginalPos();
                next.Draw(spriteBatch);
                transition = false;
                return;
            }
            if (delay == 0)
            {
                //shift rooms for transition
                roomShift();
            }
            else
            {
                delay--;
            }
            current.Draw(spriteBatch);
            next.Draw(spriteBatch);

            
        }

        public void transtion(IRoom cur, IRoom next, int direct)
        {
            current = cur;
            this.next = next;

            cur.setTransitioning(true);
            this.next.setTransitioning(true);

            next.setTransitionSide(direct);
            direction = direct;
            //delay for rooms to be displayed side by side
            delay = 90;
            transition = true;
        }

        public void roomShift()
        {
            //next.isShiftDone();
            if (!next.isTransitioning())
            {
                //when the next room is done transitioning reset current room and stop transitioning
                current.setTransitioning(false);
                current.resetToOriginalPos();
                transition = false;
                return;
            }
            switch (direction)
            {
                case 5:
                    //do same transition as case 1
                case 0:
                    current.transitionShift(0, 2);
                    next.transitionShift(0, 2);
                    break;
                case 1:
                    current.transitionShift(2, 0);
                    next.transitionShift(2, 0);
                    break;
                case 2:
                    current.transitionShift(-2, 0);
                    next.transitionShift(-2, 0);
                    break;
                case 4:
                    //do same trasition as case 3
                case 3:
                    current.transitionShift(0, -2);
                    next.transitionShift(0, -2);
                    break;
            }
        }

        public Boolean transitioning()
        {
            return transition;
        }
    }
}
