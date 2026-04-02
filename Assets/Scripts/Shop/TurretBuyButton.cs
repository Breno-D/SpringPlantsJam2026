using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretBuyButton : MonoBehaviour
{
    [SerializeField] GameObject turretPrefab;
    [SerializeField] TextMeshProUGUI turretPriceText;
    Button buttonAttached;

    void Awake()
    {
        buttonAttached = GetComponent<Button>();
    }

    void OnEnable()
    {
        int turretCost = turretPrefab.GetComponent<TurretCombat>().turret.turretCost;
        turretPriceText.text = turretCost.ToString();
        if(ShopManager.instance.GetCoins() >= turretCost)
        {
            buttonAttached.interactable = true;
        }
        else
        {
            buttonAttached.interactable = false;
        }
    }

    public void TurretSelect()
    {
        ShopManager.instance.StartBuyingTurret(turretPrefab);
    }
}
