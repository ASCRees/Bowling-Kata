using System;

namespace BowlingScoring.Interfaces
{
    public interface IScore
    {
        public int CurrentScore { get; set; }

        public void SetScore(int v);
    }
}