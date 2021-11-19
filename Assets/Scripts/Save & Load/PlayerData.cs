using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public int health;
    public int gems;

    public PlayerData(PlayerInfo player)
    {
        this.health = player.health;

    }
}