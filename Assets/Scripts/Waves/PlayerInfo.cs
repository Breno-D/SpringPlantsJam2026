using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;
    [SerializeField] int playerHealth;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] List<GameObject> heartsObjects = new List<GameObject>();
    int totalPoints;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
 
    public void TakeDamage(int damageToTake)
    {
        playerHealth -= damageToTake;
        UpdateHealthUI();
        if(playerHealth <= 0)
        {
            GameOverPanel.SetActive(true);
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        totalPoints += pointsToAdd;
        UpdatePointsUI();
    }

    void UpdatePointsUI()
    {
        pointsText.text = "Points: " + totalPoints;
    }

    void UpdateHealthUI()
    {
        int heartCount = 1;
        foreach(GameObject heart in heartsObjects)
        {
            if(playerHealth >= heartCount)
            {
                heart.SetActive(true);
            }
            else
            {
                heart.SetActive(false);
            }
            heartCount++;
        }
    }
}
