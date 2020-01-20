using BowlingScoring.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoring
{
    public class Game:IGame
    {
        public List<IPlayersGame> PlayersGames { get; set; }

        public Game()
        {
            PlayersGames = new List<IPlayersGame>();  
        }

        public void InitializeGameForPlayer(string playersName)
        {
            if (string.IsNullOrWhiteSpace(playersName))
            {
                throw new ArgumentNullException("Players name cannot be blank");
            }

            var playersGame = new PlayersGame()
            {
                Name = playersName
            };

            playersGame.BuildPlayersFrames();

            PlayersGames.Add(playersGame);

        }
        public void RunGame()
        {

        }
    }
}
