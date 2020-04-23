// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using System;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps.Legacy;
using osu.Game.Rulesets.Configuration;
using osu.Game.Configuration;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Replays.Types;
using osu.Game.Rulesets.Touhou.Beatmaps;
using osu.Game.Rulesets.Touhou.Configuration;
using osu.Game.Rulesets.Touhou.Difficulty;
using osu.Game.Rulesets.Touhou.Mods;
using osu.Game.Rulesets.Touhou.Replays;
using osu.Game.Rulesets.Touhou.Scoring;
using osu.Game.Rulesets.Touhou.UI;

namespace osu.Game.Rulesets.Touhou
{
    public class TouhouRuleset : Ruleset
    {
        public TouhouHealthProcessor HealthProcessor { get; private set; }

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) => new DrawableTouhouRuleset(this, beatmap, mods);

        public override ScoreProcessor CreateScoreProcessor() => new TouhouScoreProcessor();

        public override HealthProcessor CreateHealthProcessor(double drainStartTime) => HealthProcessor = new TouhouHealthProcessor();

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) => new TouhouBeatmapConverter(beatmap, this);

        public override IConvertibleReplayFrame CreateConvertibleReplayFrame() => new TouhouReplayFrame();

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.Left, TouhouAction.MoveLeft),
            new KeyBinding(InputKey.Right, TouhouAction.MoveRight),
            new KeyBinding(InputKey.Up, TouhouAction.MoveUp),
            new KeyBinding(InputKey.Down, TouhouAction.MoveDown),
            new KeyBinding(InputKey.Z, TouhouAction.Shoot1),
            new KeyBinding(InputKey.MouseLeft, TouhouAction.Shoot1),
            new KeyBinding(InputKey.X, TouhouAction.Shoot2),
            new KeyBinding(InputKey.MouseRight, TouhouAction.Shoot2)
        };

        public override IEnumerable<Mod> ConvertFromLegacyMods(LegacyMods mods)
        {
            if (mods.HasFlag(LegacyMods.Nightcore))
                yield return new TouhouModNightcore();
            else if (mods.HasFlag(LegacyMods.DoubleTime))
                yield return new TouhouModDoubleTime();

            if (mods.HasFlag(LegacyMods.SuddenDeath))
                yield return new TouhouModSuddenDeath();

            if (mods.HasFlag(LegacyMods.NoFail))
                yield return new TouhouModNoFail();

            if (mods.HasFlag(LegacyMods.HalfTime))
                yield return new TouhouModHalfTime();

            if (mods.HasFlag(LegacyMods.Easy))
                yield return new TouhouModEasy();

            if (mods.HasFlag(LegacyMods.Relax))
                yield return new TouhouModRelax();

            if (mods.HasFlag(LegacyMods.Hidden))
                yield return new TouhouModHidden();

            if (mods.HasFlag(LegacyMods.Flashlight))
                yield return new TouhouModFlashlight();

            if (mods.HasFlag(LegacyMods.Cinema))
                yield return new TouhouModCinema();
            else if (mods.HasFlag(LegacyMods.Autoplay))
                yield return new TouhouModAutoplay();
        }

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.DifficultyReduction:
                    return new Mod[]
                    {
                        new TouhouModEasy(),
                        new TouhouModNoFail(),
                        new MultiMod(new TouhouModHalfTime(), new TouhouModDaycore())
                    };

                case ModType.DifficultyIncrease:
                    return new Mod[]
                    {
                        new TouhouModSuddenDeath(),
                        new MultiMod(new TouhouModDoubleTime(), new TouhouModNightcore()),
                        new TouhouModHidden(),
                        new TouhouModFlashlight()
                    };

                case ModType.Automation:
                    return new Mod[]
                    {
                        new MultiMod(new TouhouModAutoplay(), new TouhouModCinema()),
                        new TouhouModRelax(),
                    };

                case ModType.Fun:
                    return new Mod[]
                    {
                        new MultiMod(new ModWindUp(), new ModWindDown()),
                    };

                default:
                    return Array.Empty<Mod>();
            }
        }

        public override string Description => "osu!touhou";

        public override string ShortName => "touhou";

        public override string PlayingVerb => "Avoiding apples";

        public override Drawable CreateIcon() => new Sprite
        {
            Texture = new TextureStore(new TextureLoaderStore(CreateResourceStore()), false).Get("Textures/logo"),
        };

        public override DifficultyCalculator CreateDifficultyCalculator(WorkingBeatmap beatmap) => new TouhouDifficultyCalculator(this, beatmap);

        public override IRulesetConfigManager CreateConfig(SettingsStore settings) => new TouhouRulesetConfigManager(settings, RulesetInfo);

        public override RulesetSettingsSubsection CreateSettings() => new TouhouSettingsSubsection(this);
    }
}
