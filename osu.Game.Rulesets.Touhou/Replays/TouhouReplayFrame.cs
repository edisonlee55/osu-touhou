using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Replays.Legacy;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Replays.Types;

namespace osu.Game.Rulesets.Touhou.Replays
{
    public class TouhouReplayFrame : ReplayFrame, IConvertibleReplayFrame
    {
        public List<TouhouAction> Actions = new List<TouhouAction>();
        public float Position;
        public bool Jumping;
        public bool Shooting;

        public TouhouReplayFrame()
        {
        }

        public TouhouReplayFrame(double time, float? position = null, bool jumping = false, bool shooting = false, TouhouReplayFrame lastFrame = null)
            : base(time)
        {
            Position = position ?? -100;
            Jumping = jumping;
            Shooting = shooting;

            if (Jumping)
                Actions.Add(TouhouAction.Jump);

            if (Shooting)
                Actions.Add(TouhouAction.Shoot);

            if (lastFrame != null)
            {
                if (Position > lastFrame.Position)
                    lastFrame.Actions.Add(TouhouAction.MoveRight);
                else if (Position < lastFrame.Position)
                    lastFrame.Actions.Add(TouhouAction.MoveLeft);
            }
        }

        public void FromLegacy(LegacyReplayFrame currentFrame, IBeatmap beatmap, ReplayFrame lastFrame = null)
        {
            Position = currentFrame.Position.X;
            Jumping = currentFrame.ButtonState == ReplayButtonState.Left1;
            Shooting = currentFrame.ButtonState == ReplayButtonState.Left2;

            if (Jumping)
                Actions.Add(TouhouAction.Jump);

            if (Shooting)
                Actions.Add(TouhouAction.Shoot);

            if (lastFrame is TouhouReplayFrame lastTouhouFrame)
            {
                if (Position > lastTouhouFrame.Position)
                    lastTouhouFrame.Actions.Add(TouhouAction.MoveRight);
                else if (Position < lastTouhouFrame.Position)
                    Actions.Add(TouhouAction.MoveLeft);
            }
        }

        public LegacyReplayFrame ToLegacy(IBeatmap beatmap)
        {
            ReplayButtonState state = ReplayButtonState.None;

            if (Actions.Contains(TouhouAction.Jump)) state |= ReplayButtonState.Left1;
            if (Actions.Contains(TouhouAction.Shoot)) state |= ReplayButtonState.Left2;

            return new LegacyReplayFrame(Time, Position, null, state);
        }
    }
}
