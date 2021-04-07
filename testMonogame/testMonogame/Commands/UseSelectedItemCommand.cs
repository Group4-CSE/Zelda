using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Commands
{
    class UseSelectedItemCommand : ICommand
    {
        GameManager game;
        public UseSelectedItemCommand(GameManager inGame)
        {
            game = inGame;
        }
        public void Execute()
        {
            IPlayer p = game.getPlayer();
            String selected = p.GetSelectedItem();
            switch (selected)
            {
                case "Bomb":
                    p.UseBomb(game);
                    break;
                case "Boomerang":
                    p.UseBoomerang(game);
                    break;
                case "Bow":
                    p.UseBow(game);
                    break;
                default:
                    break;
            }
        }
    }
}
