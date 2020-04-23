using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Touhou.Configuration;

namespace osu.Game.Rulesets.Touhou.UI
{
    public class TouhouSettingsSubsection : RulesetSettingsSubsection
    {
        protected override string Header => "osu!touhou";

        public TouhouSettingsSubsection(Ruleset ruleset)
            : base(ruleset)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            var config = (TouhouRulesetConfigManager)Config;

            Children = new Drawable[]
            {
                new SettingsEnumDropdown<PlayerModel>
                {
                    LabelText = "Player model",
                    Bindable = config.GetBindable<PlayerModel>(TouhouRulesetSetting.PlayerModel)
                },
                new SettingsEnumDropdown<BackgroundType>
                {
                    LabelText = "Background type",
                    Bindable = config.GetBindable<BackgroundType>(TouhouRulesetSetting.Background)
                },
                new SettingsSlider<double>
                {
                    LabelText = "Playfield dim",
                    Bindable = config.GetBindable<double>(TouhouRulesetSetting.PlayfieldDim),
                    KeyboardStep = 0.01f,
                    DisplayAsPercentage = true
                },
                new SettingsSlider<double>
                {
                    LabelText = "Slider opacity",
                    Bindable = config.GetBindable<double>(TouhouRulesetSetting.SliderOpacity),
                    KeyboardStep = 0.01f,
                    DisplayAsPercentage = true
                }
            };
        }
    }
}
