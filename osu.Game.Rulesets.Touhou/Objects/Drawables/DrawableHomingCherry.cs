using osu.Game.Rulesets.Touhou.Extensions;

namespace osu.Game.Rulesets.Touhou.Objects.Drawables
{
    public class DrawableHomingCherry : DrawableAngledCherry
    {
        public DrawableHomingCherry(HomingCherry h)
            : base(h)
        {
        }

        protected override float GetAngle() => MathExtensions.GetPlayerAngle(Player, HitObject.Position);
    }
}
