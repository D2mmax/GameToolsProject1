// Same thing here, using the shared LeaderboardData class
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class TimerAndLeaderboard : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private string levelName;
    [SerializeField] private Collider startTrigger;
    [SerializeField] private Collider endTrigger;

    private float startTime;
    private bool isTimerRunning;

    private string leaderboardPath;

    void Start()
    {
        leaderboardPath = Application.persistentDataPath + "/leaderboard.json";
        if (timerText != null)
        {
            timerText.text = "0:00:000";
        }
        isTimerRunning = false;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            float currentTime = Time.time - startTime;
            timerText.text = FormatTime(currentTime);
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        float finalTime = Time.time - startTime;
        SaveTime(finalTime);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        return $"{minutes}:{seconds:00}:{milliseconds:000}";
    }

    private void SaveTime(float time)
    {
        LeaderboardData leaderboardData = new LeaderboardData();
        if (File.Exists(leaderboardPath))
        {
            string json = File.ReadAllText(leaderboardPath);
            leaderboardData = JsonUtility.FromJson<LeaderboardData>(json);
        }

        leaderboardData.records.Add(new LeaderboardRecord
        {
            playerName = "Player", // Replace with the actual player name
            levelName = levelName,
            time = time
        });

        string updatedJson = JsonUtility.ToJson(leaderboardData, true);
        File.WriteAllText(leaderboardPath, updatedJson);
    }
}
