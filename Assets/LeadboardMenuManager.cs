// Now uses the shared LeaderboardData class
using UnityEngine;
using TMPro;
using System.IO;

public class LeaderboardMenuManager : MonoBehaviour
{
    public TMP_Text tutorialText;
    public TMP_Text level1Text;
    public TMP_Text level2Text;
    public TMP_Text level3Text;
    public GameObject leaderboardPanel;
    private string leaderboardPath;

    void Start()
    {
        leaderboardPath = Application.persistentDataPath + "/leaderboard.json";
    }

    // Method to display leaderboard for each level
// Method to display leaderboard for each level
public void ShowLeaderboard()
{
    if (File.Exists(leaderboardPath))
    {
        string json = File.ReadAllText(leaderboardPath);
        LeaderboardData leaderboardData = JsonUtility.FromJson<LeaderboardData>(json);

        // Prepare leaderboard texts
        string tutorialLeaderboard = "";
        string level1Leaderboard = "";
        string level2Leaderboard = "";
        string level3Leaderboard = "";

        // Sort the leaderboard by time (ascending)
        leaderboardData.records.Sort((x, y) => x.time.CompareTo(y.time));

        // Display top 5 for each level
        int rankTutorial = 1, rank1 = 1, rank2 = 1, rank3 = 1;

        // Loop through all records to categorize them by level
        foreach (LeaderboardRecord record in leaderboardData.records)
        {
            if (rankTutorial > 5 && rank1 > 5 && rank2 > 5 && rank3 > 5) break; // Show only top 5 for each level

            // Check each level and update the respective leaderboard
            if (record.levelName == "Tutorial" && rankTutorial <= 5)
            {
                tutorialLeaderboard += $"{rankTutorial}. {record.playerName} - {FormatTime(record.time)}\n";
                rankTutorial++;
            }
            else if (record.levelName == "Level1" && rank1 <= 5)
            {
                level1Leaderboard += $"{rank1}. {record.playerName} - {FormatTime(record.time)}\n";
                rank1++;
            }
            else if (record.levelName == "Level2" && rank2 <= 5)
            {
                level2Leaderboard += $"{rank2}. {record.playerName} - {FormatTime(record.time)}\n";
                rank2++;
            }
            else if (record.levelName == "Level3" && rank3 <= 5)
            {
                level3Leaderboard += $"{rank3}. {record.playerName} - {FormatTime(record.time)}\n";
                rank3++;
            }
        }

        // Set the leaderboard text for each level (without repeating level names)
        tutorialText.text = (rankTutorial == 1) ? "No data found for Tutorial." : tutorialLeaderboard;
        level1Text.text = (rank1 == 1) ? "No data found for Level 1." : level1Leaderboard;
        level2Text.text = (rank2 == 1) ? "No data found for Level 2." : level2Leaderboard;
        level3Text.text = (rank3 == 1) ? "No data found for Level 3." : level3Leaderboard;

        // Show the leaderboard panel
        leaderboardPanel.SetActive(true);
    }
    else
    {
        // No leaderboard data available
        tutorialText.text = "No data found for Tutorial.";
        level1Text.text = "No data found for Level 1.";
        level2Text.text = "No data found for Level 2.";
        level3Text.text = "No data found for Level 3.";
        leaderboardPanel.SetActive(true);
    }
}



    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        return $"{minutes}:{seconds:00}:{milliseconds:000}";
    }
}
