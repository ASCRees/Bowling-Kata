using System;

namespace BowlingScoring.Interfaces
{
    public interface IScore
    {
        public int CurrentScore { get; set; }

        public void SetScore(Int32 currScore, bool isFirstBowl, Int32 frameNumber);
    }
}