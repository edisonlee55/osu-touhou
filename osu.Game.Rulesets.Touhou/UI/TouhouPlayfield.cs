using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
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
        private readonly Sprite failSprite;

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
                },
                failSprite = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Width = 0.7f,
                    Alpha = 0,
                    FillMode = FillMode.Fit,
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            failSprite.Texture = textures.Get("game-over");
        }

        private bool failInvoked;

        protected override void Update()
        {
            base.Update();

            if (!healthProcessor.HasFailed)
                return;

            if (failInvoked)
                return;

            onFail();
            failInvoked = true;
        }

        private void onFail()
        {
            failSprite.FadeIn();
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
