using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Touhou.Judgements;

namespace osu.Game.Rulesets.Touhou.Objects
{
    public class Cherry : TouhouHitObject
    {
        public float CircleSize { get; set; } = 1;

        public override Judgement CreateJudgement() => new NullJudgement();

        protected override void ApplyDefaultsToSelf(ControlPointInfo controlPointInfo, BeatmapDifficulty difficulty)
        {
            base.ApplyDefaultsToSelf(controlPointInfo, difficulty);
            CircleSize = difficulty.CircleSize;
        }
    }
}
