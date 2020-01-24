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

            switch (isFirstBowl)
            {
                case true:
                    currentFrame.FirstPins = currScore;

                    // If the frame is at least the 2nd frame check the previous frame
                    // if previous frame was a spare update the bonustotal with the currscore
                    // if previous frame was a strike update the bonustotal with the currscore
                    // If this is at least the 3rd frame then check the frame prior to that to
                    // see if it was a strike also then update with the currscore

                    if (frameNumber > 1)
                    {
                        UpdatePreviousFrame(currScore, frameNumber, isFirstBowl, GetFrameForPlayer(frameNumber - 1), currentFrame.IsBonusFrame);
                    }

                    break;

                case false:
                    currentFrame.SecondPins = currScore;
                    // If the current frame is at least the 2nd frame check the previous frame if it was a strike
                    // update the total with the sub-total from this frame

                    if (frameNumber > 1)
                    {
                        UpdatePreviousFrame(currScore, frameNumber, isFirstBowl, GetFrameForPlayer(frameNumber - 1), currentFrame.IsBonusFrame);
                    }

                    if (!currentFrame.IsSpare)
                    {
                        currentFrame.FrameTotal = currentFrame.SubTotal;
                    }

                    break;
            };

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