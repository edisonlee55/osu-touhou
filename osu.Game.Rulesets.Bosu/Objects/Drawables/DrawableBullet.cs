﻿using osu.Framework.Graphics;
using osu.Game.Rulesets.Bosu.UI.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osuTK;
using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Rulesets.Bosu.Objects.Drawables
{
    public class DrawableBullet : DrawableBosuHitObject
    {
        private const int bullet_size = 12;

        private bool isMoving;
        private readonly float angle;

        private readonly Sprite sprite;
        private readonly Sprite overlay;

        public DrawableBullet(Bullet h)
            : base(h)
        {
            angle = h.Angle;

            Origin = Anchor.Centre;
            Size = new Vector2(bullet_size);
            Position = h.Position;
            Scale = Vector2.Zero;
            Alpha = 0;
            AlwaysPresent = true;

            AddInternal(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    sprite = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                    },
                    overlay = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                    }
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            sprite.Texture = textures.Get("apple");
            overlay.Texture = textures.Get("apple-overlay");

            AccentColour.BindValueChanged(accent => sprite.Colour = accent.NewValue, true);
        }

        protected override void UpdateInitialTransforms()
        {
            base.UpdateInitialTransforms();

            this.ScaleTo(Vector2.One, HitObject.TimePreempt);
            this.FadeIn(HitObject.TimePreempt).Finally(_ => isMoving = true);
        }

        protected override void Update()
        {
            base.Update();

            if (!isMoving)
                return;

            var xDelta = Clock.ElapsedFrameTime * Math.Sin(angle * Math.PI / 180) / 5f;
            var yDelta = Clock.ElapsedFrameTime * -Math.Cos(angle * Math.PI / 180) / 5f;

            Position = new Vector2(Position.X + (float)xDelta, Position.Y + (float)yDelta);
        }

        protected override void UpdateStateTransforms(ArmedState state)
        {
            base.UpdateStateTransforms(state);

            switch (state)
            {
                case ArmedState.Idle:
                    break;

                case ArmedState.Hit:
                    this.FadeOut(200);
                    break;

                case ArmedState.Miss:
                    this.FadeOut();
                    break;
            }
        }

        protected override bool CheckCollision(BosuPlayer player)
        {
            var playerPosition = player.PlayerPositionInPlayfieldSpace();

            if (playerPosition.X + player.PlayerDrawSize().X / 2 > Position.X - bullet_size / 2 && playerPosition.X - player.PlayerDrawSize().X / 2 < Position.X + bullet_size / 2
                && playerPosition.Y + player.PlayerDrawSize().Y / 2 > Position.Y - bullet_size / 2 && playerPosition.Y - player.PlayerDrawSize().Y / 2 < Position.Y + bullet_size / 2)
                return true;

            return false;
        }
    }
}
