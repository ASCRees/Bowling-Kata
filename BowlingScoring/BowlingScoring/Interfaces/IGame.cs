namespace BowlingScoring.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IGame
    {
        public List<IPlayersGame> PlayersGames { get; set; }

        public void InitializeGameForPlayer(string playersName);
        public void RunGame();
        public bool CheckPlayerIsComplete(IPlayersGame playerGame, Int32 currentFrameNumber);

        public Int32 Bowl(Int32 pinsInPlay);
        public Int32 RunBowl(Int32 remainingNumPins);
    }
}
