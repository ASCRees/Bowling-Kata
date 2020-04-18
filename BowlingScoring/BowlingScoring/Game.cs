namespace BowlingScoring
{
    using System;
    using System.Collections.Generic;
    using BowlingScoring.Interfaces;

    public class Game : IGame
    {
        public List<IPlayersGame> PlayersGamesList { get; set; }
        private IBowl _bowlingBall;

        public Game(IBowl bowlingBall)
        {
            _bowlingBall = bowlingBall;
            PlayersGamesList = new List<IPlayersGame>();
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

            PlayersGamesList.Add(playersGame);
        }

        public void RunGame()
        {
            Int32 currentFrameNumber = 0;

            while (currentFrameNumber < 10)
            {
                foreach (IPlayersGame playerGame in PlayersGamesList)
                {
                    PlayPlayersGo(currentFrameNumber, playerGame);

                    AddAndPlayBonusFrame(currentFrameNumber, playerGame);
                }

                currentFrameNumber++;
            }
        }

        private void PlayPlayersGo(int currentFrameNumber, IPlayersGame playersGame)
        {
            var pinsStillStanding = 10;

            var firstBowl = true;

            while (!playersGame.PlayersFrames[currentFrameNumber].IsComplete)
            {
                pinsStillStanding = ScorePlayersBowl(currentFrameNumber, playersGame, pinsStillStanding, firstBowl);

                firstBowl = false;
            }
        }

        private int ScorePlayersBowl(int currentFrameNumber, IPlayersGame playersGame, int pinsStillStanding, bool firstBowl)
        {
            // Take bowl

            var bowledPins = _bowlingBall.BowlBall(pinsStillStanding);

            pinsStillStanding = 10 - bowledPins;

            // Score Pins
            IScore bowlingscore = new Score(playersGame);

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

        public bool CheckPlayerIsComplete(IPlayersGame playerGame, Int32 currentFrameNumber)
        {
            return playerGame.PlayersFrames[currentFrameNumber].IsComplete;
        }
    }
}