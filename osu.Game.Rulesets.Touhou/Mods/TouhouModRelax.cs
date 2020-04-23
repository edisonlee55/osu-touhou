using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Touhou.Objects;
using osu.Game.Rulesets.Touhou.UI;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Touhou.Mods
{
    public class TouhouModRelax : ModRelax, IApplicableToDrawableRuleset<TouhouHitObject>
    {
        public override string Description => @"Use the mouse to control the player.";

        public void ApplyToDrawableRuleset(DrawableRuleset<TouhouHitObject> drawableRuleset)
        {
            drawableRuleset.Cursor.Add(new MouseInputHelper((TouhouPlayfield)drawableRuleset.Playfield));
        }

        private class MouseInputHelper : Drawable, IKeyBindingHandler<TouhouAction>, IRequireHighFrequencyMousePosition
        {
            private readonly Container player;

            public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => true;

            public MouseInputHelper(TouhouPlayfield playfield)
            {
                player = playfield.Player.Player;
                RelativeSizeAxes = Axes.Both;
            }

            public bool OnPressed(TouhouAction action)
            {
                switch (action)
                {
                    case TouhouAction.MoveLeft:
                    case TouhouAction.MoveRight:
                    case TouhouAction.MoveUp:
                    case TouhouAction.MoveDown:
                        return true;
                }

                return false;
            }

            public void OnReleased(TouhouAction action)
            {
                switch (action)
                {
                    case TouhouAction.MoveLeft:
                    case TouhouAction.MoveRight:
                    case TouhouAction.MoveUp:
                    case TouhouAction.MoveDown:
                        return;
                }
            }

            protected override bool OnMouseMove(MouseMoveEvent e)
            {
                player.X = e.MousePosition.X / DrawSize.X * TouhouPlayfield.BASE_SIZE.X;
                player.Y = e.MousePosition.Y / DrawSize.Y * TouhouPlayfield.BASE_SIZE.Y;
                return base.OnMouseMove(e);
            }
        }
    }
}
