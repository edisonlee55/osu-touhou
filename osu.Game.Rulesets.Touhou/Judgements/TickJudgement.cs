﻿using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Touhou.Judgements
{
    public class TickJudgement : TouhouJudgement
    {
        protected override double HealthIncreaseFor(HitResult result)
        {
            switch (result)
            {
                case HitResult.Miss:
                    return -0.05f;
            }

            return base.HealthIncreaseFor(result);
        }

        protected override int NumericResultFor(HitResult result)
        {
            switch (result)
            {
                default:
                    return 0;

                case HitResult.Perfect:
                    return 100;
            }
        }
    }
}
