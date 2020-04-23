using System.Collections.Generic;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Touhou.Replays;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Touhou.UI
{
    public class TouhouReplayRecorder : ReplayRecorder<TouhouAction>
    {
        private readonly TouhouPlayfield playfield;

        public TouhouReplayRecorder(Replay target, TouhouPlayfield playfield)
            : base(target)
        {
            this.playfield = playfield;
        }

        protected override ReplayFrame HandleFrame(Vector2 mousePosition, List<TouhouAction> actions, ReplayFrame previousFrame)
            => new TouhouReplayFrame(Time.Current, playfield.Player.PlayerPosition(), new[] { actions.Contains(TouhouAction.Shoot1), actions.Contains(TouhouAction.Shoot2) }, previousFrame as TouhouReplayFrame);
    }
}
