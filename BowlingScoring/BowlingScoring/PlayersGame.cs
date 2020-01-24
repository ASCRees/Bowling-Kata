namespace BowlingScoring
{
    using BowlingScoring.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayersGame : IPlayersGame
    {
        public string Name { get; set; }
        public List<IFrame> PlayersFrames { get; set; }

        public Int32 PlayerTotal
        {
            get
            {
                if (PlayersFrames.Count > 0)
                {
                    return PlayersFrames.Where(c => c.FrameNumber <= 10).Sum(x => x.FrameTotal);
                }
                else
                {
                    return 0;
                }
            }
        }

        public PlayersGame()
        {
            PlayersFrames = new List<IFrame>();
        }

        public void BuildPlayersFrames()
        {
            for (int i = 0; i < 10; i++)
            {
                PlayersFrames.Add(new Frame()
                {
                    FrameNumber = i + 1
                }); ;
            }
        }

        public void AddBonusFrame()
        {
            PlayersFrames.Add(new Frame()
            {
                FrameNumber = 11,
                IsBonusFrame = true
            }); ;
        }
    }
}