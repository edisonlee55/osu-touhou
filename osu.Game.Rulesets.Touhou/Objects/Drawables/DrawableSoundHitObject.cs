using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Touhou.Objects.Drawables
{
    public class DrawableSoundHitObject : DrawableTouhouHitObject
    {
        public DrawableSoundHitObject(SoundHitObject h)
            : base(h)
        {
        }

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (timeOffset > 0)
                ApplyResult(r => r.Type = HitResult.Meh);
        }
    }
}
