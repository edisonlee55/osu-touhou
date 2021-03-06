﻿using osuTK;
using System;
using osu.Game.Rulesets.Scoring;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Touhou.Extensions;
using osu.Game.Rulesets.Touhou.UI;
using osu.Game.Rulesets.Touhou.UI.Objects;

namespace osu.Game.Rulesets.Touhou.Objects.Drawables
{
    public class DrawableAngledCherry : DrawableCherry
    {
        protected readonly float SpeedMultiplier;

        private readonly float finalSize;

        public DrawableAngledCherry(AngledCherry h)
            : base(h)
        {
            AlwaysPresent = true;
            SpeedMultiplier = (float)(MathExtensions.Map((float)h.SpeedMultiplier, 0, 3, 0.9f, 1.1f) * h.DeltaMultiplier / 4.5f );
            finalSize = Size.X;
        }

        protected virtual float GetAngle() => ((AngledCherry)HitObject).Angle;

        private double missTime;

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (timeOffset > 0)
            {
                if (collidedWithPlayer(Player))
                {
                    Player.PlayMissAnimation();
                    missTime = timeOffset;
                    ApplyResult(r => r.Type = HitResult.Miss);
                    return;
                }

                if (timeOffset > GetWallCheckOffset())
                {
                    if (Position.X > TouhouPlayfield.BASE_SIZE.X + DrawSize.X / 2f
                    || Position.X < -DrawSize.X / 2f
                    || Position.Y > TouhouPlayfield.BASE_SIZE.Y + DrawSize.Y / 2f
                    || Position.Y < -DrawSize.Y / 2f)
                        ApplyResult(r => r.Type = HitResult.Perfect);
                }
            }
        }

        protected virtual float GetWallCheckOffset() => 0;

        private bool collidedWithPlayer(TouhouPlayer player)
        {
            // Let's assume that player is a circle for simplicity

            var playerPosition = player.PlayerPosition();
            var distance = Math.Sqrt(Math.Pow(Math.Abs(playerPosition.X - Position.X), 2) + Math.Pow(Math.Abs(playerPosition.Y - Position.Y), 2));
            var playerRadius = player.PlayerDrawSize().X / 2f;
            var cherryRadius = finalSize / 2f;

            if (distance < playerRadius + cherryRadius)
                return true;

            return false;
        }

        private float? angle;

        protected override void Update()
        {
            base.Update();

            Vector2 newPosition = (Time.Current > HitObject.StartTime) ? UpdatePosition(Time.Current) : HitObject.Position;

            if (newPosition == Position)
                return;

            Position = newPosition;
        }

        protected virtual Vector2 UpdatePosition(double currentTime)
        {
            if (angle == null)
                angle = GetAngle();

            var elapsedTime = Clock.CurrentTime - HitObject.StartTime;
            var xPosition = HitObject.Position.X + (elapsedTime * SpeedMultiplier * Math.Sin((angle ?? 0) * Math.PI / 180));
            var yPosition = HitObject.Position.Y + (elapsedTime * SpeedMultiplier * -Math.Cos((angle ?? 0) * Math.PI / 180));
            return new Vector2((float)xPosition, (float)yPosition);
        }

        protected override void UpdateStateTransforms(ArmedState state)
        {
            base.UpdateStateTransforms(state);

            switch (state)
            {
                case ArmedState.Miss:
                    // Check DrawableHitCircle L#168
                    this.Delay(missTime).FadeOut();
                    break;
            }
        }
    }
}
