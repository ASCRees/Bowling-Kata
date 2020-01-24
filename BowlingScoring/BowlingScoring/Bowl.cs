using BowlingScoring.Interfaces;
using System;

namespace BowlingScoring
{
    public class Bowl : IBowl
    {
        public Int32 BowlBall(Int32 pinsInPlay)
        {
            return new Random().Next(0, pinsInPlay);
        }
    }
}