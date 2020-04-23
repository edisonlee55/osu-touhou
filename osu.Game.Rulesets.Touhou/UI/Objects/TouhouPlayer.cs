using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osuTK;
using System;
using osuTK.Graphics;
using osu.Framework.Input.Bindings;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.Bindables;
using osu.Framework.Audio.Sample;
using osu.Framework.Audio.Track;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Touhou.Configuration;
using osu.Game.Rulesets.Touhou.Replays;

namespace osu.Game.Rulesets.Touhou.UI.Objects
{
    public class TouhouPlayer : CompositeDrawable, IKeyBindingHandler<TouhouAction>
    {
        private const double base_speed = 1.0 / 4.5;

        [Resolved]
        private TextureStore textures { get; set; }

        private SampleChannel shoot;

        private readonly Bindable<PlayerModel> playerModel = new Bindable<PlayerModel>();

        public override bool RemoveCompletedTransforms => false;

        private int horizontalDirection;
        private int verticalDirection;

        public readonly Container Player;
        private readonly Sprite drawablePlayer;
        private readonly Container bulletsContainer;

        public TouhouPlayer()
        {
            RelativeSizeAxes = Axes.Both;
            AddRangeInternal(new Drawable[]
            {
                bulletsContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both
                },
                Player = new Container
                {
                    Origin = Anchor.Centre,
                    Size = new Vector2(15),
                    Child = drawablePlayer = new Sprite
                    {
                        RelativeSizeAxes = Axes.Both
                    }
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load(TouhouRulesetConfigManager config, ISampleStore samples)
        {
            config.BindWith(TouhouRulesetSetting.PlayerModel, playerModel);

            shoot = samples.Get("shoot");
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            playerModel.BindValueChanged(model =>
            {
                switch (model.NewValue)
                {
                    case PlayerModel.Boshy:
                        drawablePlayer.Texture = textures.Get("Player/boshy");
                        return;

                    case PlayerModel.Kid:
                        drawablePlayer.Texture = textures.Get("Player/kid");
                        return;

                    default:
                        drawablePlayer.Texture = textures.Get("Player/bosu");
                        return;
                }
            }, true);

            Player.Position = new Vector2(TouhouPlayfield.BASE_SIZE.X / 2f, TouhouPlayfield.BASE_SIZE.Y - PlayerDrawSize().Y / 2f);
        }

        public Vector2 PlayerPosition(Vector2? offset = null) => new Vector2(Player.Position.X + (offset?.X ?? 0), Player.Position.Y - (offset?.Y ?? 0));

        public Vector2 PlayerDrawSize() => drawablePlayer.DrawSize;

        public void PlayMissAnimation() => drawablePlayer.FlashColour(Color4.Red, 1000, Easing.OutQuint);

        public bool OnPressed(TouhouAction action)
        {
            switch (action)
            {
                case TouhouAction.MoveLeft:
                    horizontalDirection--;
                    return true;

                case TouhouAction.MoveRight:
                    horizontalDirection++;
                    return true;

                case TouhouAction.MoveUp:
                    verticalDirection--;
                    return true;

                case TouhouAction.MoveDown:
                    verticalDirection++;
                    return true;

                case TouhouAction.Shoot1:
                    onShoot();
                    return true;

                case TouhouAction.Shoot2:
                    onShoot();
                    return true;
            }

            return false;
        }

        public void OnReleased(TouhouAction action)
        {
            switch (action)
            {
                case TouhouAction.MoveLeft:
                    horizontalDirection++;
                    return;

                case TouhouAction.MoveRight:
                    horizontalDirection--;
                    return;

                case TouhouAction.MoveUp:
                    verticalDirection++;
                    return;

                case TouhouAction.MoveDown:
                    verticalDirection--;
                    return;

                case TouhouAction.Shoot1:
                    return;

                case TouhouAction.Shoot2:
                    return;
            }
        }

        protected override void Update()
        {
            updateReplayState();

            base.Update();

            if (horizontalDirection != 0)
            {
                var positionX = Math.Clamp(Player.X + Math.Sign(horizontalDirection) * Clock.ElapsedFrameTime * base_speed, PlayerDrawSize().X / 2f, TouhouPlayfield.BASE_SIZE.X - PlayerDrawSize().X / 2f);

                // Player.Scale = new Vector2(Math.Abs(Scale.X) * (horizontalDirection > 0 ? 1 : -1), Player.Scale.Y);

                if (positionX == Player.X)
                    return;

                Player.X = (float)positionX;
            }

            if (verticalDirection != 0)
            {
                var positionY = Math.Clamp(Player.Y + Math.Sign(verticalDirection) * Clock.ElapsedFrameTime * base_speed, PlayerDrawSize().Y / 2f, TouhouPlayfield.BASE_SIZE.Y - PlayerDrawSize().Y / 2f);

                // Player.Scale = new Vector2(Player.Scale.X, Math.Abs(Scale.Y) * (verticalDirection > 0 ? 1 : -1));

                if (positionY == Player.Y)
                    return;

                Player.Y = (float)positionY;
            }
        }

        private void onShoot()
        {
            shoot.Play();
            bulletsContainer.Add(new Bullet(Player.Scale.X > 0, Clock.CurrentTime)
            {
                Position = PlayerPosition(new Vector2(0, 1))
            });
        }

        private void updateReplayState()
        {
            var state = (GetContainingInputManager().CurrentState as RulesetInputManagerInputState<TouhouAction>)?.LastReplayState as TouhouFramedReplayInputHandler.TouhouReplayState ?? null;

            if (state != null)
            {
                Player.X = state.Position.Value.X;
                Player.Y = state.Position.Value.Y;
            }
        }
    }
}
