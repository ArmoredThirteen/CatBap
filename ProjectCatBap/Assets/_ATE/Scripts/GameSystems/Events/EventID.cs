namespace ATE.Events
{
    public enum EventID
    {
        // Win/Loss
        WinLevel,
        LoseLevel,

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