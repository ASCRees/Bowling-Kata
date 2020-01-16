﻿using BowlingScoring.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoring
{
    public class Frame:IFrame
    {
        Int32 frameNumber = 1;
        Int32 firstScore = 0;
        Int32 secondScore = 0;

        public Int32 FrameNumber {
            get
            {
                return frameNumber;
            }
            set
            {

                if (value > 1 || value < 11)
                {
                    throw new ArgumentOutOfRangeException("Frame Number can only be between 1 and 11");
                }

                frameNumber = value;

            }
        }

        public Int32 FirstScore { 
            get {
                    return firstScore;
                }
            set
            {
 
                if (value > 10 || value < 0)
                {
                    throw new ArgumentOutOfRangeException("Score can only be between 0 and 10");
                }

                firstScore = value;

            }
        }

        public bool FirstBowl { get; set; }

        public Int32 SecondScore
        {
            get
            {
                return secondScore;
            }
            set
            {
                if (FirstBowl)
                {
                    throw new ArgumentException("Its not the second bowl");
                }
                else
                {

                    if (value > 10 || value < 0)
                    {
                        throw new ArgumentOutOfRangeException("Score can only be between 0 and 10");
                    }
                    if ((value + firstScore) > 10)
                    {
                        throw new ArgumentOutOfRangeException("Total Score for the frame cannot be greater than 10");
                    }
                }
                secondScore = value;

            }
        }
        public Int32 SubTotal
        {
            get
            {
                return firstScore + secondScore;
            }
        }


        public bool IsSpare { get; set; }
        public bool IsStrike { get; set; }

    }
}