namespace BowlingScoring.Interfaces
{
    using System;
    using System.Collections.Generic;
    public interface IPlayersGame
    {
        public string Name { get; set; }

        public List<IFrame> PlayersFrames { get; set; }
        public Int32 PlayerTotal { get; }

        public void BuildPlayersFrames();

        public void AddBonusFrame();
    }
}