using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    int health;
    int gems;
    int highscore;

    void Start()
    {
        health = SaveSystem.LoadPlayer().health;
        highscore = SaveSystem.LoadPlayer().highscore;
    }

    public PlayerData(HealthSystem player, Inventory inventory)
    {
        this.health = player.currentHealth;
        this.highscore = inventory.highScore;
    }

    public void LoadPlayerHealth(HealthSystem player)
    {
        player.currentHealth = this.health;
    }

    public void LoadInventory(Inventory inventory)
    {
        inventory.highScore = this.highscore;
    }
}