using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    class GoriyaEnemy
    {
        int x;
        int y;
        int health;
        public IGoriyaState state;
        Texture2D texture;
        Texture2D projTexture;

        public GoriyaEnemy(Texture2D inTexture, Texture2D inProjTexture)
        {
            texture = inTexture;
            projTexture = inProjTexture;
            state = new GoriyaWD(texture, projTexture, this);
            health = 3;
        }

        public void Move(int xChange, int yChange)
        {
            x += xChange;
            y += yChange;
        }

        public void takeDamage(int dmg)
        {
            health -= dmg;
        }

        public void Attack(IPlayer player)
        {
            // Collide with player = attack
        }



        public void changeState(int direction)
        {
            switch (direction)
            {
                case 1:
                    state = new GoriyaWD(texture, projTexture, this);
                    break;
                case 2:
                    state = new GoriyaWU(texture, projTexture, this);
                    break;
                case 3:
                    state = new GoriyaWL(texture, projTexture, this);
                    break;
                case 4:
                    state = new GoriyaWR(texture, projTexture, this);
                    break;
                default:
                    state = new GoriyaWD(texture, projTexture, this);
                    break;
            }
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }
    }
}
