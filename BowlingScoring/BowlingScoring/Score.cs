using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowlingScoring.Interfaces;

namespace BowlingScoring
{
    public class Score:IScore
    {
        public int CurrentScore { get; set; }

        public void SetScore(int currScore)
        {
            CurrentScore = currScore;
        }
    }
}
