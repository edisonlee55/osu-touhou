using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;
using System.Collections.Generic;
using osu.Game.Rulesets.Touhou.Objects;
using osu.Game.Rulesets.Touhou.Objects.Drawables;
using osu.Game.Rulesets.Touhou.Replays;

namespace osu.Game.Rulesets.Touhou.UI
{
    public class DrawableTouhouRuleset : DrawableRuleset<TouhouHitObject>
    {
        public DrawableTouhouRuleset(Ruleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
        }

        protected override PassThroughInputManager CreateInputManager() => new TouhouInputManager(Ruleset.RulesetInfo);

        protected override Playfield CreatePlayfield() => new TouhouPlayfield(((TouhouRuleset)Ruleset).HealthProcessor);

        public override PlayfieldAdjustmentContainer CreatePlayfieldAdjustmentContainer() => new TouhouPlayfieldAdjustmentContainer();

        protected override ReplayRecorder CreateReplayRecorder(Replay replay) => new TouhouReplayRecorder(replay, (TouhouPlayfield)Playfield);

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new TouhouFramedReplayInputHandler(replay);

        public override DrawableHitObject<TouhouHitObject> CreateDrawableRepresentation(TouhouHitObject h)
        {
            switch (h)
            {
                case SoundHitObject sound:
                    return new DrawableSoundHitObject(sound);

                case SliderPartCherry sliderPart:
                    return new DrawableSliderPartCherry(sliderPart);

                case EndTimeCherry endTime:
                    return new DrawableEndTimeCherry(endTime);

                case TickCherry tick:
                    return new DrawableTickCherry(tick);

                case HomingCherry homing:
                    return new DrawableHomingCherry(homing);

                case AngledCherry angled:
                    return new DrawableAngledCherry(angled);
            }

            return null;
        }
    }
}
