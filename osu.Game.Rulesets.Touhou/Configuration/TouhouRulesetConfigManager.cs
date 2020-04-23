using osu.Game.Configuration;
using osu.Game.Rulesets.Configuration;

namespace osu.Game.Rulesets.Touhou.Configuration
{
    public class TouhouRulesetConfigManager : RulesetConfigManager<TouhouRulesetSetting>
    {
        public TouhouRulesetConfigManager(SettingsStore settings, RulesetInfo ruleset, int? variant = null)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();
            Set(TouhouRulesetSetting.PlayerModel, PlayerModel.Bosu);
            Set(TouhouRulesetSetting.Background, BackgroundType.None);
            Set(TouhouRulesetSetting.PlayfieldDim, 0.5, 0, 1);
            Set(TouhouRulesetSetting.SliderOpacity, 0.0, 0, 1);
        }
    }

    public enum TouhouRulesetSetting
    {
        PlayerModel,
        Background,
        PlayfieldDim,
        SliderOpacity
    }

    public enum PlayerModel
    {
        Bosu,
        Boshy,
        Kid
    }

    public enum BackgroundType
    {
        None,
        Red,
        White
    }
}
