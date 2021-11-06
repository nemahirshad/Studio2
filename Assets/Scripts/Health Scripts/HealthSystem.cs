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
  

    Button respawn;
    Button mainMenu;

    void Start()
    {
        //currentHealth = maxHealth;
        //This code might not be necessary if we want the player's HP to carry over from one level to another.
        gameOver.SetActive(false);
    }

    void Update()
    {
        //Checking if the player health exceeds the maximum amount of health they can have.
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        


/*---------------Testing HP System-----------------
        if (Input.GetKeyDown(KeyCode.Space))
            LoseHP(damage);

        if (Input.GetKeyDown(KeyCode.H))
            GainHP(gainHP);
---------------Testing HP System-----------------*/


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
        /*
        //--------------------This part of the code isn't working for some reason--------------------

                //Checks if the damage dealt is less than the max health and the player's current health, and then subtracts it from the current health.
                if(damage < maxHealth && damage < currentHealth)
                {
                    currentHealth -= damage;
                }

                /*For some reason everytime I try to test this piece of code, the player always ends up with zero hp.
                 Not sure why though. 
                Without this if-statement, the player won't lose any hp if the damage recieved is less than the max health AND less than the health the player has.

        //--------------------This part of the code isn't working for some reason--------------------


                //Checks to see if the damage dealt is more than the hp the player has left but still less than the max hp
                if (damage < maxHealth && damage >= currentHealth)
                {
                    currentHealth = 0;
                    PlayerDeath();
                }

                //Checks to see if the damage dealt is greater than the max health. If it is, it lets the player have one heart, to ensure they have a chance at survival.
                if (damage >= maxHealth)
                {
                    currentHealth -= maxHealth - 1;

                    if (currentHealth < 0)
                    {
                        currentHealth = 0;
                        PlayerDeath();
                    }
                }*/

        //Checks to see if the damage dealt is greater than the max health. If it is, it lets the player have one heart, to ensure they have a chance at survival.
        /*if (damage >= maxHealth)
        {
            currentHealth -= maxHealth - 1;

            if (currentHealth < 0)
            {
                currentHealth = 0;
                PlayerDeath();
            }
        }*/

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
        if(currentHealth <= 0)
        {
            gameOver.SetActive(true);
        }
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
