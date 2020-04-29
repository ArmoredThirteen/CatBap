namespace ATE.Events
{
    public enum EventID
    {
        // Game data
        LoadGame,
        SaveGame,
        WipeGame,

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