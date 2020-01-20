using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoring.Interfaces
{
    public interface IFrame
    {
        public Int32 FrameNumber { get; set; }
        public Int32? FirstScore { get; set; }
        public bool FirstBowl { get; set; }
        public Int32? SecondScore { get; set; }
        public bool IsSpare { get; }
        public bool IsStrike { get; }
        public Int32 SubTotal { get; }
        public bool IsComplete { get; }

    }
}
