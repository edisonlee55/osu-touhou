using osu.Game.Beatmaps;
using osu.Game.Rulesets.Mods;
using osu.Game.Scoring;
using osu.Game.Users;
using System;
using osu.Game.Rulesets.Touhou.Objects;
using osu.Game.Rulesets.Touhou.Replays;

namespace osu.Game.Rulesets.Touhou.Mods
{
    public class TouhouModCinema : ModCinema<TouhouHitObject>
    {
        public override Score CreateReplayScore(IBeatmap beatmap) => new Score
        {
            ScoreInfo = new ScoreInfo { User = new User { Username = "osu!touhou" } },
            Replay = new TouhouAutoGenerator(beatmap).Generate(),
        };

        public override Type[] IncompatibleMods => new[]
        {
            typeof(ModFlashlight)
        };
    }
}
