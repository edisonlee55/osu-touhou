using osu.Framework.Input.StateChanges;
using osu.Framework.Utils;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using System.Collections.Generic;
using osuTK;

namespace osu.Game.Rulesets.Touhou.Replays
{
    public class TouhouFramedReplayInputHandler : FramedReplayInputHandler<TouhouReplayFrame>
    {
        public TouhouFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        protected override bool IsImportant(TouhouReplayFrame frame) => true;

        protected Vector2? Position
        {
            get
            {
                var frame = CurrentFrame;

                if (frame == null)
                    return null;

                return NextFrame != null ? Interpolation.ValueAt(CurrentTime.Value, frame.Position, NextFrame.Position, frame.Time, NextFrame.Time) : frame.Position;
            }
        }

        public override List<IInput> GetPendingInputs()
        {
            if (!Position.HasValue) return new List<IInput>();

            return new List<IInput>
            {
                new TouhouReplayState
                {
                    PressedActions = CurrentFrame?.Actions ?? new List<TouhouAction>(),
                    Position = Position.Value
                },
            };
        }

        public class TouhouReplayState : ReplayState<TouhouAction>
        {
            public Vector2? Position { get; set; }
        }
    }
}
