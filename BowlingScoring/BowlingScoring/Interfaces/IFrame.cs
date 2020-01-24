using System;

namespace BowlingScoring.Interfaces
{
    public interface IFrame
    {
        public Int32 FrameNumber { get; set; }
        public Int32? FirstPins { get; set; }
        public Int32? SecondPins { get; set; }

        //public Int32? FirstScore { get; set; }
        //public Int32? SecondScore { get; set; }
        public bool FirstBowl { get; set; }

        public bool IsSpare { get; }
        public bool IsStrike { get; }
        public Int32 SubTotal { get; }
        public bool IsComplete { get; }
        public Int32 BonusTotal { get; set; }
        public Int32 FrameTotal { get; set; }
        public bool IsBonusFrame { get; set; }
    }
}