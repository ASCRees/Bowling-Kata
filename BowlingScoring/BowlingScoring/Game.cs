using BowlingScoring.Interfaces;
using System;
using System.Collections.Generic;

namespace BowlingScoring
{
    public class Game : IGame
    {
        public List<IPlayersGame> PlayersGames { get; set; }
        private IBowl _bowlingBall;

        public Game()
        {
            _bowlingBall = new Bowl();
            PlayersGames = new List<IPlayersGame>();
        }

        public Game(IBowl bowlingBall)
        {
            _bowlingBall = bowlingBall;
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

            while (currentFrameNumber < 10)
            {
                foreach (IPlayersGame playerGame in PlayersGames)
                {
                    PlayPlayersGo(currentFrameNumber, playerGame);

                    AddAndPlayBonusFrame(currentFrameNumber, playerGame);
                }

                currentFrameNumber++;
            }
        }

        private void PlayPlayersGo(int currentFrameNumber, IPlayersGame playerGame)
        {
            var pinsStillStanding = 10;

            var firstBowl = true;

            while (!playerGame.PlayersFrames[currentFrameNumber].IsComplete)
            {
                pinsStillStanding = ScorePlayersBowl(currentFrameNumber, playerGame, pinsStillStanding, firstBowl);

                firstBowl = false;
            }
        }

        private int ScorePlayersBowl(int currentFrameNumber, IPlayersGame playerGame, int pinsStillStanding, bool firstBowl)
        {
            // Take bowl

            var bowledPins = _bowlingBall.BowlBall(pinsStillStanding);

            pinsStillStanding = 10 - bowledPins;

            // Score Pins
            IScore bowlingscore = new Score(playerGame);

            bowlingscore.SetScore(bowledPins, firstBowl, currentFrameNumber + 1);
            return pinsStillStanding;
        }

        private void AddAndPlayBonusFrame(int currentFrameNumber, IPlayersGame playerGame)
        {
            if (currentFrameNumber == 9 && (playerGame.PlayersFrames[currentFrameNumber].IsStrike || playerGame.PlayersFrames[currentFrameNumber].IsSpare))
            {
                // Add an extra frame
                playerGame.AddBonusFrame();

                PlayPlayersGo(currentFrameNumber + 1, playerGame);
            }
        }

        public Int32 Bowl(Int32 pinsInPlay)
        {
            return new Random().Next(0, pinsInPlay);
        }

        public bool CheckPlayerIsComplete(IPlayersGame playerGame, Int32 currentFrameNumber)
        {
            return playerGame.PlayersFrames[currentFrameNumber].IsComplete;
        }
    }
}