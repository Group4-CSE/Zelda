using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using testMonogame.Rooms;
using testMonogame.Interfaces;
using System.Diagnostics;

namespace testMonogame
{
    public class GameManager
    {
        Game1 game;
        IPlayer player;
        HUD hud;
        ItemSelectionScreen itemScreen;
        PauseScreen pause;
        GameOverScreen gameOver;
        WinScreen win;

        private RoomLoader roomLoad;
        Dictionary<String, IRoom> rooms = new Dictionary<string, IRoom>();
        String roomKey = "";
        RoomTransition transitioner;
        int doorCollideCountdown = 0;

        

        enum GameState {PLAYING,//0
            ITEMSELECTION,//1
            PAUSE,//2
            LOSE, //3
            WIN, //4
            ROOMTRANSITION //5
        };
        GameState state;

        Sounds sound;
        Dictionary<String, Texture2D> sprites = new Dictionary<string, Texture2D>();

        //collision detectors
        EnemyObjectCollision EOCol = new EnemyObjectCollision();
        PlayerWallCollision PWCol = new PlayerWallCollision();
        EnemyWallCollision EWCol = new EnemyWallCollision();
        PlayerProjectileWallCollision PPWCol = new PlayerProjectileWallCollision();
        //PlayerProjectileBlockCollision PPBCol = new PlayerProjectileBlockCollision();
        PlayerProjectileEnemyCollision PPECol = new PlayerProjectileEnemyCollision();
        //EnemyProjectileWallCollision EPWCol = new EnemyProjectileWallCollision();
        PlayerObjectCollision POCol = new PlayerObjectCollision();
        PlayerEnemyCollision PECol = new PlayerEnemyCollision();
        EnemyProjectileCollisionHandler EPCol;


        public GameManager(Game1 game, Dictionary<String, Texture2D> spriteSheet, SpriteFont font, SpriteFont header, Sounds sounds)
        {
            this.game = game;
            sprites = spriteSheet;
            state = GameState.PLAYING;

            //load room 17 first

            sound = sounds;

            roomLoad = new RoomLoader(sprites);
            rooms.Add("Room17", roomLoad.Load("Room17.txt"));
            roomKey = "Room17";
            transitioner = new RoomTransition();

            player = new Player(spriteSheet["playersheet"], new Vector2(500, 200), spriteSheet["PlayerProjectiles"], sound);
            hud = new HUD(spriteSheet["hudSheet"], font);
            itemScreen = new ItemSelectionScreen(spriteSheet["ItemSelection"]);
            pause = new PauseScreen(spriteSheet["MenuScreens"], font, header);
            gameOver = new GameOverScreen(spriteSheet["MenuScreens"], font, header);
            win = new WinScreen(spriteSheet["MenuScreens"]);


            EPCol = new EnemyProjectileCollisionHandler(this);


        }

        public void Update()
        {
            if (transitioner.transitioning())
            {
                return;
            }
            //PLAYING
            if (state == GameState.PLAYING)
            {
                hud.Update(this);
                player.Update(this);
                rooms[roomKey].Update(this);
                EOCol.detectCollision(rooms[roomKey].GetEnemies(), rooms[roomKey].GetBlocks());
                PWCol.detectCollision(player, rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect());
                EWCol.detectCollision(rooms[roomKey].GetEnemies(), rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect());
                PPWCol.detectCollision(rooms[roomKey].GetPlayerProjectiles(), rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect(), this);
                //PPBCol.detectCollision(rooms[roomKey].GetPlayerProjectiles(), rooms[roomKey].GetBlocks(), this);
                PPECol.detectCollision(rooms[roomKey].GetPlayerProjectiles(), rooms[roomKey].GetEnemies(), this, rooms[roomKey], sound);
                //EPWCol.detectCollision(rooms[roomKey].GetEnemeyProjectile(), rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect(), this);
                if (doorCollideCountdown <= 0)
                {
                    POCol.detectCollision(player, rooms[roomKey].GetItems(), rooms[roomKey].GetBlocks(), rooms[roomKey], this);
                }
                else
                {
                    doorCollideCountdown--;
                }
                    
                PECol.playerEnemyDetection(player, rooms[roomKey].GetEnemies(), rooms[roomKey], sound);
                EPCol.handleEnemyProjCollision(rooms[roomKey], player);
                
            }
            //Item selection
            else if (state == GameState.ITEMSELECTION)
            {
                hud.Update(this);
                player.Update(this);
                itemScreen.Update(this);
            }
            //NO UPDATES FOR PAUSE OR GAME OVER
            else if (state == GameState.WIN)
            {
                player.Update(this);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (transitioner.transitioning())
            {
                transitioner.Draw(spriteBatch);
                return;
            }

            //NORMAL STUFF
            if (state == GameState.PLAYING)
            {
                if (hud.hudY != 0) hud.hudY = 0;
                hud.Draw(spriteBatch);
                rooms[roomKey].Draw(spriteBatch);
                player.Draw(spriteBatch);
                
            }
            //item selection
            else if (state == GameState.ITEMSELECTION)
            {
                if(hud.hudY!= (176 * 2)) hud.hudY = (176 * 2);
                hud.Draw(spriteBatch);
                itemScreen.Draw(spriteBatch, rooms.Keys);
            }
            //Pause Screen
            else if (state == GameState.PAUSE)
            {
                pause.Draw(spriteBatch);
            }
            //game over
            else if (state == GameState.LOSE)
            {
                gameOver.Draw(spriteBatch);
            }
            else if (state == GameState.WIN)
            {
                win.Draw(spriteBatch);
                player.Draw(spriteBatch);
            }
        }

        public void AddEnemyProjectile(IEnemyProjectile projectile) { rooms[roomKey].AddEnemyProjectile(projectile); }
        public void RemoveEnemyProjectile(IEnemyProjectile projectile) { rooms[roomKey].RemoveEnemyProjectile(projectile); }
        public void AddPlayerProjectile(IPlayerProjectile projectile) { rooms[roomKey].AddPlayerProjectile(projectile); }
        public void RemovePlayerProjectile(IPlayerProjectile projectile) { rooms[roomKey].RemovePlayerProjectile(projectile); }

        public void LoadRoom(int roomNum)
        {
            //Debug.WriteLine(player.X);
            String name = "Room" + roomNum;
            if (rooms.ContainsKey(name))
            {
                //roomKey = name;
                ChangeRoom(roomNum);
            }
            else
            {
                rooms.Add(name, roomLoad.Load(name + ".txt"));
                //roomKey = name;
                ChangeRoom(roomNum);
            }
            //player.X = 400;
            //player.Y = 324;
        }

        public void ChangeRoom(int roomNum)
        {
            
            String curRoom = roomKey;
            String name = "Room" + roomNum;
            roomKey = name;
            int direction = -1;
            doorCollideCountdown = 5;
            foreach (IObject block in rooms[curRoom].GetBlocks())
            {
                if ((block is CaveDoor || block is ClosedDoor || block is OpenDoor || block is LockedDoor))
                {
                    IDoor door = (IDoor)block;

                    if (door.getNextRoom() == roomNum)
                    {
                        direction = door.getSide();
                        break;
                    }

                }
            }

            //make sure the door on other side is opened
            unlockNextDoor(direction);
            transitioner.transtion(rooms[curRoom], rooms[roomKey], direction);
                
        }

        public void unlockNextDoor(int direction)
        {
            switch (direction)
            {
                case 0:
                    foreach (IObject block in rooms[roomKey].GetBlocks())
                    {
                        if ((block is CaveDoor || block is ClosedDoor || block is OpenDoor || block is LockedDoor))
                        {
                            IDoor door = (IDoor)block;

                            //make sure door on the other side of the other room is also open
                            if (door.getSide() == 3)
                            {
                                door.openDoor();
                            }

                        }
                    }
                    break;
                case 1:
                    foreach (IObject block in rooms[roomKey].GetBlocks())
                    {
                        if ((block is CaveDoor || block is ClosedDoor || block is OpenDoor || block is LockedDoor))
                        {
                            IDoor door = (IDoor)block;

                            //make sure door on the other side of the other room is also open
                            if (door.getSide() == 2)
                            {
                                door.openDoor();
                            }

                        }
                    }
                    break;
                case 2:
                    foreach (IObject block in rooms[roomKey].GetBlocks())
                    {
                        if ((block is CaveDoor || block is ClosedDoor || block is OpenDoor || block is LockedDoor))
                        {
                            IDoor door = (IDoor)block;

                            //make sure door on the other side of the other room is also open
                            if (door.getSide() == 1)
                            {
                                door.openDoor();
                            }

                        }
                    }
                    break;
                case 3:
                    foreach (IObject block in rooms[roomKey].GetBlocks())
                    {
                        if ((block is CaveDoor || block is ClosedDoor || block is OpenDoor || block is LockedDoor))
                        {
                            IDoor door = (IDoor)block;

                            //make sure door on the other side of the other room is also open
                            if (door.getSide() == 0)
                            {
                                door.openDoor();
                            }

                        }
                    }
                    break;
            }
        }

        public IPlayer getPlayer()
        {
            return player;

        }
        public HUD getHUD()
        {
            return hud;
        }

        public void Exit()
        {
            game.Exit();
        }

        public void s2reset()
        {
            //for sprint 2 reset
            game.s2reset();
        }
        public int getState()
        {
            return (int)state ;
        }
        public void SetState(int inState) { state = (GameState)inState; }
    }
}
