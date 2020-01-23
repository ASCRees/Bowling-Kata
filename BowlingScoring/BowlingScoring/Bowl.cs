using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowlingScoring.Interfaces;

namespace BowlingScoring
{
    public class Bowl:IBowl
    {
        public Int32 BowlBall(Int32 pinsInPlay)
        {
            return new Random().Next(0, pinsInPlay);
        }
    }
}
