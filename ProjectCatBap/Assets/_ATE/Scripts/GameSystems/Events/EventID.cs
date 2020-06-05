namespace ATE.Events
{
    public enum EventID
    {
        // Game data
        LoadGame,
        SaveGame,
        WipeGame,
        GameLoaded,
        GameSaved,
        GameWiped,

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
        NoiseChanged,
        NoiseLevelChanged,
    }
}