namespace osu.Game.Rulesets.Touhou.Objects.Drawables
{
    public class DrawableTickCherry : DrawableHomingCherry
    {
        protected override float GetBaseSize() => 15;

        public DrawableTickCherry(TickCherry h)
            : base(h)
        {
        }
    }
}
