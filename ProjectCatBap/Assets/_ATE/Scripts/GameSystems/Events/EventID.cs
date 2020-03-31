namespace ATE.Events
{
    public enum EventID
    {
        // Level interaction
        LoadLevel,
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