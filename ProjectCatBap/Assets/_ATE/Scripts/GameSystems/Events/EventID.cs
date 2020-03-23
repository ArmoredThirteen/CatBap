namespace ATE.Events
{
    public enum EventID
    {
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