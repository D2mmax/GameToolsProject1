using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Ensure TextMeshPro is imported

public class TimerAndLeaderboard : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText; // Assign your timer UI Text (TextMeshPro) in the Inspector
    [SerializeField] private string levelName; // Assign the level name in the Inspector
    [SerializeField] private Collider startTrigger; // Assign the start trigger in the Inspector
    [SerializeField] private Collider endTrigger; // Assign the end trigger in the Inspector

    private float startTime;
    private bool isTimerRunning;

    private string leaderboardPath; // Path for the JSON file

    void Start()
    {
        // Set up the JSON file path
        leaderboardPath = Application.persistentDataPath + "/leaderboard.json";
        Debug.Log("Leaderboard Path: " + leaderboardPath);

        // Initialize the timer text to show "0:00:000"
        if (timerText != null)
        {
            timerText.text = "0:00:000";
        }

        isTimerRunning = false;
    }

    void Update()
    {
        // Update the timer if it’s running
        if (isTimerRunning)
        {
            float currentTime = Time.time - startTime;
            timerText.text = FormatTime(currentTime);
        }
    }

    public void StartTimer()
    {
        Debug.Log("Timer Started!");
        startTime = Time.time;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        Debug.Log("Timer Stopped!");
        isTimerRunning = false;
        float finalTime = Time.time - startTime;
        SaveTime(finalTime); // Save time to leaderboard
    }

    private string FormatTime(float time)
    {
        // Format the time to minutes:seconds:milliseconds
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        return $"{minutes}:{seconds:00}:{milliseconds:000}";
    }

    private void SaveTime(float time)
    {
        Debug.Log($"Saving Time: {FormatTime(time)} for Level: {levelName}");

        // Load existing leaderboard data or create a new one
        LeaderboardData leaderboardData = new LeaderboardData();
        if (System.IO.File.Exists(leaderboardPath))
        {
            string json = System.IO.File.ReadAllText(leaderboardPath);
            leaderboardData = JsonUtility.FromJson<LeaderboardData>(json);
        }

        // Add new time to the leaderboard
        leaderboardData.records.Add(new LeaderboardRecord
        {
            playerName = "Player", // Replace with actual player name if available
            levelName = levelName,
            time = time
        });

        // Save updated leaderboard back to the file
        string updatedJson = JsonUtility.ToJson(leaderboardData, true);
        System.IO.File.WriteAllText(leaderboardPath, updatedJson);

        Debug.Log("Leaderboard updated successfully!");
    }
}

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
