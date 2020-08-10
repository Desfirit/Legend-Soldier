using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SessionStats
{
    public string SessionDate;
    public EndGameState WinOrLoss;
    public int HighestLevel;
    public int EnemyKilled;
    public int ExperienceGained;
    public int WavesCompleted;

    public SessionStats()
    {
        HighestLevel = 1;
    }
}

[System.Serializable]
public enum EndGameState
{
    Win,
    Loss
}
