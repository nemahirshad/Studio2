using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int currentHealth;          //Used to show how many hearts a player still has. 
    public int maxHealth;       //Used to set the max no. of hearts a player can have.

    public Image[] hearts;

    public int damage;          //This is mostly for testing purposes.
    public int gainHP;
    public GameObject gameOver;

    public PlayerData data;

    void Start()
    {
        if (SaveSystem.FileFound())
        {
            data.LoadPlayerHealth(this);
        }
        else
        {
            currentHealth = maxHealth;
        }
        //currentHealth = maxHealth;
        //This code might not be necessary if we want the player's HP to carry over from one level to another.
        gameOver.SetActive(false);
    }

    void Update()
    {
        //Checking if the player health exceeds the maximum amount of health they can have.
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            //This is to ensure that the no. of hearts visible to the player corresponds to the amount of HP they have.
            if (i < currentHealth)
            {
                hearts[i].enabled = true;
            }

            if(i >= currentHealth)
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void LoseHP(int damage)
    {
        //Checks if the damage dealt is less than max health. If true, the player loses hp that is equal to the amount of damage recieved.
        if (damage < maxHealth)
        {
            currentHealth -= damage;

            //Checks if the player's hp is less than or equal to zero and ensures that it doesn't go lower than zero.
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                PlayerDeath();
            }
        }
    }

    public void GainHP(int hp)
    {
        currentHealth += hp;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void PlayerDeath( )
    {
       gameOver.SetActive(true);
    }
    public void RespawnButton()
    {
        SceneManager.LoadScene("SpaceShip");
    }
    public void MainMenuButton()
    {
        //SceneManager.LoadScene("Mainmenu");
    }
}
