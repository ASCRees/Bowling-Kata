namespace BowlingScoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BowlingScoring.Interfaces;
    public class Score:IScore
    {
        IFrame _currentFrame;

        public Score(IFrame currentFrame)
        {
            _currentFrame = currentFrame;
        }

        public int CurrentScore { get; set; }

        public void SetScore(Int32 currScore, bool IsFirstBowl)
        {

            CurrentScore = currScore;
        }
    }
}
