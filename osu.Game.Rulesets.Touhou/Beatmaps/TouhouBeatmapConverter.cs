using osu.Game.Beatmaps;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Objects;
using osu.Game.Beatmaps.Timing;
using osu.Game.Rulesets.Touhou.Extensions;
using osu.Game.Rulesets.Touhou.Objects;

namespace osu.Game.Rulesets.Touhou.Beatmaps
{
    public class TouhouBeatmapConverter : BeatmapConverter<TouhouHitObject>
    {
        public TouhouBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        public override bool CanConvert() => Beatmap.HitObjects.All(h => h is IHasPosition);

        private int index = -1;
        private int objectIndexInCurrentCombo = 0;

        protected override IEnumerable<TouhouHitObject> ConvertHitObject(HitObject obj, IBeatmap beatmap)
        {
            var comboData = obj as IHasCombo;
            if (comboData?.NewCombo ?? false)
            {
                objectIndexInCurrentCombo = 0;
                index++;
            }

            var beatmapStageIndex = getBeatmapStageIndex(beatmap, obj.StartTime);

            bool kiai = beatmap.ControlPointInfo.EffectPointAt(obj.StartTime).KiaiMode;

            List<TouhouHitObject> hitObjects = new List<TouhouHitObject>();

            switch (obj)
            {
                case IHasCurve curve:
                    hitObjects.AddRange(CherriesExtensions.ConvertSlider(obj, beatmap, curve, kiai, index));
                    break;

                case IHasEndTime endTime:
                    hitObjects.AddRange(CherriesExtensions.ConvertSpinner(obj, endTime, kiai, index, beatmapStageIndex));
                    break;

                default:
                    hitObjects.AddRange(CherriesExtensions.ConvertHitCircle(obj, kiai, index, objectIndexInCurrentCombo));
                    break;
            }

            objectIndexInCurrentCombo++;

            return hitObjects;
        }

        protected override Beatmap<TouhouHitObject> CreateBeatmap() => new TouhouBeatmap();

        private static int getBeatmapStageIndex(IBeatmap beatmap, double time)
        {
            if (beatmap.Breaks.Count == 0)
                return 1;

            BreakPeriod latestBreak = null;

            beatmap.Breaks.ForEach(b =>
            {
                if (b.EndTime < time)
                    latestBreak = b;
            });

            if (latestBreak == null)
                return 1;

            return beatmap.Breaks.IndexOf(latestBreak) + 1;
        }
    }
}
