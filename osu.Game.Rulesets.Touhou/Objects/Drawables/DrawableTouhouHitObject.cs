using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Touhou.UI.Objects;

namespace osu.Game.Rulesets.Touhou.Objects.Drawables
{
    public abstract class DrawableTouhouHitObject : DrawableHitObject<TouhouHitObject>
    {
        protected TouhouPlayer Player;

        protected DrawableTouhouHitObject(TouhouHitObject hitObject)
            : base(hitObject)
        {
        }

        protected sealed override double InitialLifetimeOffset => HitObject.TimePreempt;

        public void GetPlayerToTrace(TouhouPlayer player) => Player = player;

        protected override void UpdateStateTransforms(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Idle:
                    // Manually set to reduce the number of future alive objects to a bare minimum.
                    LifetimeStart = HitObject.StartTime - HitObject.TimePreempt;
                    break;
            }
        }
    }
}
