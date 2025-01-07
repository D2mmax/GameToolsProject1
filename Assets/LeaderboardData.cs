// LeaderboardDataClasses.cs
using System;
using System.Collections.Generic;

[Serializable]
public class LeaderboardData
{
    public List<LeaderboardRecord> records = new List<LeaderboardRecord>();
}

[Serializable]
public class LeaderboardRecord
{
    public string playerName;
    public string levelName;
    public float time;
}
