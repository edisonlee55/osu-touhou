using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Touhou.Judgements;

namespace osu.Game.Rulesets.Touhou.Objects
{
    public class TickCherry : HomingCherry
    {
        public override Judgement CreateJudgement() => new TickJudgement();

        protected override void ApplyDefaultsToSelf(ControlPointInfo controlPointInfo, BeatmapDifficulty difficulty)
        {
            base.ApplyDefaultsToSelf(controlPointInfo, difficulty);
            TimePreempt = 0;
        }
    }
}
