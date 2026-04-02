using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int coinsToGive;
    public int pointsToGive;

    public void TakeDamage(int damageToTake)
    {
        maxHealth -= damageToTake;
    }

    // Update is called once per frame
    void Update()
    {
        if(maxHealth <= 0)
        {
            WaveManager.instance.RemoveEnemyFromList(gameObject);
            ShopManager.instance.AddCoins(coinsToGive);
            PlayerInfo.instance.AddPoints(pointsToGive);
            Destroy(gameObject);
            return;
        }
    }
}
