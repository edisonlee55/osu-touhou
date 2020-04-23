using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Touhou.Objects;

namespace osu.Game.Rulesets.Touhou.Mods
{
    public class TouhouModNightcore : ModNightcore<TouhouHitObject>
    {
        public override double ScoreMultiplier => 1.1f;
    }
}
