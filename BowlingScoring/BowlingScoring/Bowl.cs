namespace BowlingScoring
{
    using System;
    using BowlingScoring.Interfaces;
    public class Bowl : IBowl
    {
        public Int32 BowlBall(Int32 pinsInPlay)
        {
            return new Random().Next(0, pinsInPlay);
        }
    }
}