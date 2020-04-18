namespace BowlingScoring
{
    using BowlingScoring.Interfaces;
    using System;

    public class Score : IScore
    {
        private IPlayersGame _playersGame;

        public Score(IPlayersGame playersGame)
        {
            _playersGame = playersGame;
        }

        public int CurrentScore { get; set; }

        public void SetScore(Int32 currScore, bool isFirstBowl, Int32 frameNumber)
        {
            IFrame currentFrame = GetFrameForPlayer(frameNumber);

            if (frameNumber > 1)
            {
                UpdatePreviousFrame(currScore, frameNumber, isFirstBowl, GetFrameForPlayer(frameNumber - 1), currentFrame.IsBonusFrame);
            }

            if (isFirstBowl)
            {
                currentFrame.FirstPins = currScore;
            }
            else
            {
                currentFrame.SecondPins = currScore;
                if (!currentFrame.IsSpare)
                {
                    currentFrame.FrameTotal = currentFrame.SubTotal;
                }
            }

            CurrentScore = currScore;
        }

        private void UpdatePreviousFrame(int currScore, int frameNumber, bool isFirstBowl, IFrame previousFrame, bool isBonusFrame)
        {
            if ((isFirstBowl && previousFrame.IsSpare) || previousFrame.IsStrike)
            {
                // Update the previous bonustotal
                previousFrame.BonusTotal += currScore;

                if ((isFirstBowl && previousFrame.IsSpare) || (isBonusFrame && !isFirstBowl && previousFrame.IsStrike))
                {
                    previousFrame.FrameTotal = previousFrame.SubTotal + previousFrame.BonusTotal;
                }

                // If this is the first bowl and the previous frame had a strike check the one before.
                if (isFirstBowl && previousFrame.IsStrike && frameNumber > 2)
                {
                    IFrame frameMinusTwo = GetFrameForPlayer(frameNumber - 2);
                    if (frameMinusTwo != null && frameMinusTwo.IsStrike)
                    {
                        // Update the previous bonustotal
                        frameMinusTwo.BonusTotal += currScore;
                        frameMinusTwo.FrameTotal = frameMinusTwo.SubTotal + frameMinusTwo.BonusTotal;
                    }
                }
            }
        }

        private IFrame GetFrameForPlayer(int frameNumber)
        {
            return _playersGame.PlayersFrames[frameNumber - 1];
        }
    }
}