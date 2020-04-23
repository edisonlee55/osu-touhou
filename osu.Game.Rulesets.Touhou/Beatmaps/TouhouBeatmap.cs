using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics.Sprites;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Touhou.Objects;

namespace osu.Game.Rulesets.Touhou.Beatmaps
{
    public class TouhouBeatmap : Beatmap<TouhouHitObject>
    {
        public override IEnumerable<BeatmapStatistic> GetStatistics()
        {
            var totalCount = HitObjects.Count();
            var hitCount = HitObjects.Count(s => s is AngledCherry);

            return new[]
            {
                new BeatmapStatistic
                {
                    Name = @"Cherries",
                    Content = hitCount.ToString(),
                    Icon = FontAwesome.Regular.Circle
                },
                new BeatmapStatistic
                {
                    Name = @"Visual objects",
                    Content = (totalCount - hitCount).ToString(),
                    Icon = FontAwesome.Regular.Circle
                }
            };
        }
    }
}
