using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Touhou.Objects.Drawables;
using osu.Game.Rulesets.Touhou.Scoring;
using osu.Game.Rulesets.Touhou.UI.Objects;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Touhou.UI
{
    public class TouhouPlayfield : Playfield
    {
        public static readonly Vector2 BASE_SIZE = new Vector2(512, 384);

        internal readonly TouhouPlayer Player;

        private readonly TouhouHealthProcessor healthProcessor;

        public TouhouPlayfield(TouhouHealthProcessor healthProcessor)
        {
            this.healthProcessor = healthProcessor;

            InternalChildren = new Drawable[]
            {
                new TouhouBackground(),
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        HitObjectContainer,
                        Player = new TouhouPlayer()
                    }
                }
            };
        }

        private bool failInvoked;

        protected override void Update()
        {
            base.Update();

            if (!healthProcessor.HasFailed)
                return;

            if (failInvoked)
                return;

            failInvoked = true;
        }

        public override void Add(DrawableHitObject h)
        {
            if (h is DrawableCherry drawable)
            {
                drawable.GetPlayerToTrace(Player);
                base.Add(drawable);
                return;
            }

            base.Add(h);
        }
    }
}
