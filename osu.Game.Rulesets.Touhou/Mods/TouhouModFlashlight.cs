using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Touhou.Objects;
using osu.Game.Rulesets.Touhou.UI;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Touhou.Mods
{
    public class TouhouModFlashlight : ModFlashlight<TouhouHitObject>
    {
        public override double ScoreMultiplier => 1.12;

        private const float default_flashlight_size = 250;

        public override Flashlight CreateFlashlight() => new TouhouFlashlight(playfield);

        private TouhouPlayfield playfield;

        public override void ApplyToDrawableRuleset(DrawableRuleset<TouhouHitObject> drawableRuleset)
        {
            playfield = (TouhouPlayfield)drawableRuleset.Playfield;
            base.ApplyToDrawableRuleset(drawableRuleset);
        }

        private class TouhouFlashlight : Flashlight
        {
            private readonly TouhouPlayfield playfield;

            public TouhouFlashlight(TouhouPlayfield playfield)
            {
                this.playfield = playfield;
                FlashlightSize = new Vector2(0, getSizeFor(0));
            }

            protected override void Update()
            {
                base.Update();

                var playerPos = playfield.Player.PlayerPosition();

                FlashlightPosition = playfield.ToSpaceOfOtherDrawable(new Vector2(playerPos.X, playerPos.Y - 50), this);
            }

            private float getSizeFor(int combo)
            {
                if (combo > 200)
                    return default_flashlight_size * 0.65f;
                else if (combo > 100)
                    return default_flashlight_size * 0.8f;
                else
                    return default_flashlight_size;
            }

            protected override void OnComboChange(ValueChangedEvent<int> e)
            {
                this.TransformTo(nameof(FlashlightSize), new Vector2(0, getSizeFor(e.NewValue)), FLASHLIGHT_FADE_DURATION);
            }

            protected override string FragmentShader => "CircularFlashlight";
        }
    }
}
