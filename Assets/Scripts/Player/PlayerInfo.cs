using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int health;

    public int highScore;
    public List<Score> scores = new List<Score>();


    public void AddScore(Score sc)
    {
        scores.Add(sc);
    }
}

public struct Score
{
    public string username;
    
    public int score;
}