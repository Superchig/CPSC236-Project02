using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScoreController : MonoBehaviour
{
    public int score = 0;
    public int highScore;

    void Start()
    {
        ReadScore();
        UpdateScore();
    }

    public void UpdateScore()
    {
        if (score > highScore)
        {
            highScore = score;
            // WriteScoreIfHigh();
        }

        GetComponent<Text>().text = "Score: " + score + "\nHigh Score: " + highScore;
    }

    private void ReadScore()
    {
        if (!File.Exists(@"high_score.txt"))
        {
            File.Create(@"high_score.txt").Close();
            highScore = 0;
            WriteScoreIfHigh();

            return;
        }

        using (StreamReader reader = new StreamReader(@"high_score.txt"))
        {
            highScore = int.Parse(reader.ReadLine());
        }
    }

    public void WriteScoreIfHigh()
    {
        if (score < highScore)
        {
            return;
        }

        using (StreamWriter writer = new StreamWriter(@"high_score.txt"))
        {
            writer.WriteLine(score);
        }
    }

    void OnApplicationQuit()
    {
        WriteScoreIfHigh();
    }
}
