namespace BowlingScoring
{
    using System.Collections.Generic;
    using BowlingScoring.Interfaces;
    public class PlayersGame : IPlayersGame
    {
        public string Name { get; set; }
        public List<IFrame> PlayersFrames { get; set; }

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
                FrameNumber = 11
            });
        }

    }
}