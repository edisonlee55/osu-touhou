// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;
using System.ComponentModel;

namespace osu.Game.Rulesets.Touhou
{
    public class TouhouInputManager : RulesetInputManager<TouhouAction>
    {
        public TouhouInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum TouhouAction
    {
        [Description("Move Left")]
        MoveLeft,

        [Description("Move Right")]
        MoveRight,

        [Description("Jump")]
        Jump,

        [Description("Shoot")]
        Shoot
    }
}
