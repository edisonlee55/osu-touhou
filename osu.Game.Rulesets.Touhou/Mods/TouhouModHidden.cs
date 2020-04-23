using osu.Framework.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Touhou.Objects;
using osu.Game.Rulesets.Touhou.Objects.Drawables;

namespace osu.Game.Rulesets.Touhou.Mods
{
    public class TouhouModHidden : ModHidden
    {
        public override string Description => @"Play with fading fruits.";
        public override double ScoreMultiplier => 1.06;

        protected override void ApplyHiddenState(DrawableHitObject drawable, ArmedState state)
        {
            if (!(drawable is DrawableAngledCherry))
                return;

            var drawableCherry = (DrawableAngledCherry)drawable;
            var cherry = (AngledCherry)drawableCherry.HitObject;

            using (drawableCherry.BeginAbsoluteSequence(cherry.StartTime, true))
                drawableCherry.FadeOut(cherry.TimePreempt * 1.5f);
        }
    }
}
