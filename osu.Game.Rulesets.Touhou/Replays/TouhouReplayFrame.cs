using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Replays.Legacy;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Replays.Types;
using osuTK;

namespace osu.Game.Rulesets.Touhou.Replays
{
    public class TouhouReplayFrame : ReplayFrame, IConvertibleReplayFrame
    {
        public List<TouhouAction> Actions = new List<TouhouAction>();
        public Vector2 Position;
        public bool[] Shooting = new bool[2];

        public TouhouReplayFrame()
        {
        }

        public TouhouReplayFrame(double time, Vector2? position = null, bool[] shooting = null, TouhouReplayFrame lastFrame = null)
            : base(time)
        {
            Position = position ?? new Vector2(-100, -100);
            shooting ??= new[] { false, false };
            Shooting = shooting;

            if (shooting[0])
                Actions.Add(TouhouAction.Shoot1);
            if (shooting[1])
                Actions.Add(TouhouAction.Shoot2);

            if (lastFrame != null)
            {
                if (Position.X > lastFrame.Position.X)
                    lastFrame.Actions.Add(TouhouAction.MoveRight);
                else if (Position.X < lastFrame.Position.X)
                    lastFrame.Actions.Add(TouhouAction.MoveLeft);
                if (Position.Y > lastFrame.Position.Y)
                    lastFrame.Actions.Add(TouhouAction.MoveDown);
                else if (Position.Y < lastFrame.Position.Y)
                    lastFrame.Actions.Add(TouhouAction.MoveUp);
            }
        }

        public void FromLegacy(LegacyReplayFrame currentFrame, IBeatmap beatmap, ReplayFrame lastFrame = null)
        {
            Position = currentFrame.Position;
            Shooting[0] = currentFrame.ButtonState == ReplayButtonState.Left1;
            Shooting[1] = currentFrame.ButtonState == ReplayButtonState.Right1;

            if (Shooting[0])
                Actions.Add(TouhouAction.Shoot1);
            if (Shooting[1])
                Actions.Add(TouhouAction.Shoot2);

            if (lastFrame is TouhouReplayFrame lastTouhouFrame)
            {
                if (Position.X > lastTouhouFrame.Position.X)
                    lastTouhouFrame.Actions.Add(TouhouAction.MoveRight);
                else if (Position.X < lastTouhouFrame.Position.X)
                    lastTouhouFrame.Actions.Add(TouhouAction.MoveLeft);
                if (Position.Y > lastTouhouFrame.Position.Y)
                    lastTouhouFrame.Actions.Add(TouhouAction.MoveDown);
                else if (Position.Y < lastTouhouFrame.Position.Y)
                    lastTouhouFrame.Actions.Add(TouhouAction.MoveUp);
            }
        }

        public LegacyReplayFrame ToLegacy(IBeatmap beatmap)
        {
            ReplayButtonState state = ReplayButtonState.None;

            if (Actions.Contains(TouhouAction.Shoot1)) state |= ReplayButtonState.Left1;
            if (Actions.Contains(TouhouAction.Shoot2)) state |= ReplayButtonState.Right1;

            return new LegacyReplayFrame(Time, Position.X, Position.Y, state);
        }
    }
}
