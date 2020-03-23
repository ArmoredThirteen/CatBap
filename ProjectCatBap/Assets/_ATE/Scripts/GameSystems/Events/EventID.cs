namespace ATE.Events
{
    public enum EventID
    {
        // Win/Loss
        LevelWin,
        LevelLose,

        // Points system
        AddPoints,
        RemovePoints,
        SetPoints,
        PointsChanged,

        // Noise system
        AddNoise,
        RemoveNoise,
        NoiseLevelIncrease,
        NoiseLevelDecrease,
    }
}