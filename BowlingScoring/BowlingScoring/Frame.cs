namespace BowlingScoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BowlingScoring.Interfaces;

    public class Frame:IFrame
    {
        Int32 frameNumber = 1;
        Int32? firstScore;
        Int32? secondScore;

        public Int32 FrameNumber {
            get
            {
                return frameNumber;
            }
            set
            {

                if (value < 1 || value > 11)
                {
                    throw new ArgumentOutOfRangeException("Frame Number can only be between 1 and 11");
                }

                frameNumber = value;

            }
        }

        public Int32? FirstScore { get; set; }
        public Int32? SecondScore { get; set; }
        public bool FrameTotal { get; }

        public Int32? FirstPins { 
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

        public Int32? SecondPins
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
                return Convert.ToInt32(firstScore) + Convert.ToInt32(secondScore);
            }
        }


        public bool IsSpare 
        { 
            get
            {
                return (FirstPins < 10 && (FirstPins + SecondPins) == 10);
            } 
        }

        public bool IsStrike
        {
            get
            {
                return (FirstPins ==10);
            }
        }

        public bool IsComplete 
        {
            get
            {
                return (FirstPins != null && (IsStrike || SecondPins != null));
            }
        }


    }
}
