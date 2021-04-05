using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace testMonogame
{
    public class Sounds
    {
        Song song;
        SoundEffect swordSlash;
        SoundEffect swordShoot;
        SoundEffect ArrowBoom;
        SoundEffect BombDrop;
        SoundEffect BombBlow;
        SoundEffect EnemyHit;
        SoundEffect EnemyDies;
        SoundEffect PlayerHit;
        //SoundEffect PlayerDies;
        //SoundEffect LHealth;
        SoundEffect GetItems;
        SoundEffect GetHeart;
        SoundEffect GetRupee;
        /*SoundEffect KeyAppear;
        SoundEffect DoorUnlock;
        SoundEffect AquaBoss;
        SoundEffect BossHurt;
        SoundEffect Stairs;*/


        public Sounds()
        {
            
        }

        public void LoadSounds(ContentManager contentManager)
        {
            //Loading Background Music
            song = contentManager.Load<Song>("BMu");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            //Loading All Collsion sounds
            swordSlash = contentManager.Load<SoundEffect>("LOZ_Sword_Slash");
            swordShoot = contentManager.Load<SoundEffect>("LOZ_Sword_Shoot");
            ArrowBoom = contentManager.Load<SoundEffect>("LOZ_Arrow_Boomerang");
            BombDrop = contentManager.Load<SoundEffect>("LOZ_Bomb_Drop");
            BombBlow = contentManager.Load<SoundEffect>("LOZ_Bomb_Blow");
            EnemyHit = contentManager.Load<SoundEffect>("LOZ_Enemy_Hit");
            EnemyDies = contentManager.Load<SoundEffect>("LOZ_Enemy_Die");
            PlayerHit = contentManager.Load<SoundEffect>("LOZ_Link_Hurt");
            //PlayerDies = contentManager.Load<SoundEffect>("LOZ_Link_Hurt");
            //LHealth = contentManager.Load<SoundEffect>("BMu");
            GetItems = contentManager.Load<SoundEffect>("LOZ_Get_Item");
            GetHeart = contentManager.Load<SoundEffect>("LOZ_Get_Heart");
            GetRupee = contentManager.Load<SoundEffect>("LOZ_Get_Rupee");
            //KeyAppear = contentManager.Load<SoundEffect>("BMu");
            //DoorUnlock = contentManager.Load<SoundEffect>("LOZ_Door_Unlock");
            //AquaBoss = contentManager.Load<SoundEffect>("BMu");
            //BossHurt = contentManager.Load<SoundEffect>("BMu");
            //Stairs = contentManager.Load<SoundEffect>("BMu");


        }
        public void Sword_Slash()
        {
            swordSlash.Play();
        }
        public void Sword_Shoot()
        {
            swordShoot.Play();
        }
        public void Link_Hurt()
        {
            PlayerHit.Play();
        }
        public void Arr_Boom()
        {
            ArrowBoom.Play();
        }
        public void BombD(int type)
        {
            //0 == Drop
            if(type == 0){
                BombDrop.Play();
            } else
            {
                //Bomb explode
                BombBlow.Play();
            }
        }
        public void EnemyHitDie(int type)
        {
            if (type == 0)
            {
                EnemyHit.Play();
            } else
            {
                EnemyDies.Play();
            }
        }

        public void getStuff(int type)
        {
            if (type == 0)
            {
                GetItems.Play();

            } else if (type == 1)
            {
                GetHeart.Play();

            } else if( type == 2)
            {
                GetRupee.Play();
            }
        }
            

            
        


    }
}
