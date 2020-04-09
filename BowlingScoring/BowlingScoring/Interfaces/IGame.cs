namespace BowlingScoring.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IGame
    {
        public List<IPlayersGame> PlayersGamesList { get; set; }

        public void InitializeGameForPlayer(string playersName);

        public void RunGame();

        public bool CheckPlayerIsComplete(IPlayersGame playerGame, Int32 currentFrameNumber);

    }
}