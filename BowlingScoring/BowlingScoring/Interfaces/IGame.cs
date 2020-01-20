﻿namespace BowlingScoring.Interfaces
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
    }
}
