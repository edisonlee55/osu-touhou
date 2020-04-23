using osu.Game.Beatmaps;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;
using osu.Game.Rulesets.Touhou.Beatmaps;

namespace osu.Game.Rulesets.Touhou.Replays
{
    internal class TouhouAutoGenerator : AutoGenerator
    {
        public new TouhouBeatmap Beatmap => (TouhouBeatmap)base.Beatmap;

        public TouhouAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
            Replay = new Replay();
        }

        protected Replay Replay;

        public override Replay Generate()
        {
            Replay.Frames.Add(new TouhouReplayFrame(0));
            return Replay;
        }
    }
}
