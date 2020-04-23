namespace osu.Game.Rulesets.Touhou.Objects
{
    /// <summary>
    /// Will move towards the opposite side of the player after end time until hit the borders.
    /// </summary>
    public class EndTimeCherry : AngledCherry
    {
        public double EndTime { get; set; }
    }
}
