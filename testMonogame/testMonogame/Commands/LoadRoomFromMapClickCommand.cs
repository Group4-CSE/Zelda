using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Commands
{
    class LoadRoomFromMapClickCommand : ICommand
    {
        Dictionary<String, int> LookUpTable;
        GameManager game;
        String key;
        public LoadRoomFromMapClickCommand(GameManager inGame, int inX, int inY)
        {
            LookUpTable = new Dictionary<string, int>();
            LookUpTable.Add("2,1", 1);
            LookUpTable.Add("3,1", 2);
            LookUpTable.Add("2,2", 3);
            LookUpTable.Add("3,2", 4);
            LookUpTable.Add("5,2", 5);
            LookUpTable.Add("6,2", 6);
            LookUpTable.Add("1,3", 7);
            LookUpTable.Add("2,3", 8);
            LookUpTable.Add("3,3", 9);
            LookUpTable.Add("4,3", 10);
            LookUpTable.Add("5,3", 11);
            LookUpTable.Add("2,4", 12);
            LookUpTable.Add("3,4", 13);
            LookUpTable.Add("4,4", 14);
            LookUpTable.Add("3,5", 15);
            LookUpTable.Add("2,6", 16);
            LookUpTable.Add("3,6", 17);
            LookUpTable.Add("4,6", 18);
            game = inGame;
            key = inX.ToString() + "," + inY.ToString();

        }
        public void Execute()
        {
            if (LookUpTable.ContainsKey(key)) game.LoadRoom(LookUpTable[key]);
        }
    }
}
