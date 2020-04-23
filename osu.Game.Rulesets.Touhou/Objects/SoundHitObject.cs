using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Touhou.Judgements;

namespace osu.Game.Rulesets.Touhou.Objects
{
    /// <summary>
    /// Used only for hit-sounds purposes. Will no affect combo/score.
    /// </summary>
    public class SoundHitObject : TouhouHitObject
    {
        public override Judgement CreateJudgement() => new NullJudgement();
    }
}
