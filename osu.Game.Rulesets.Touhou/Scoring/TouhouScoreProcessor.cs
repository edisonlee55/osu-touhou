using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Touhou.Scoring
{
    public class TouhouScoreProcessor : ScoreProcessor
    {
        public override HitWindows CreateHitWindows() => new TouhouHitWindows();
    }
}
