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
            Int32 currentFrameNumber = 0;
            bool gameIsNotComplete = true;

            while (gameIsNotComplete)
            {
                foreach (IPlayersGame playerGame in PlayersGames)
                {
                    if (currentFrameNumber == 9)
                    {
                        if (!CheckPlayerIsComplete(playerGame, currentFrameNumber))
                        {

                        }
                    }
                }


            }
        }

        public Int32 Bowl (Int32 pinsInPlay)
        {
            return new Random().Next(0, pinsInPlay);
        }

        public bool CheckPlayerIsComplete(IPlayersGame playerGame, Int32 currentFrameNumber )
        {
            return playerGame.PlayersFrames[currentFrameNumber].IsComplete;
        }
    }
}
