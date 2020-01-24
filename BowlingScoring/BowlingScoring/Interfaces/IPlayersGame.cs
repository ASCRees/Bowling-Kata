using System;
using System.Collections.Generic;

namespace BowlingScoring.Interfaces
{
    public interface IPlayersGame
    {
        public string Name { get; set; }

        public List<IFrame> PlayersFrames { get; set; }
        public Int32 PlayerTotal { get; }

        public void BuildPlayersFrames();

        public void AddBonusFrame();
    }
}