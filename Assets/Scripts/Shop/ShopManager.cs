using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public List<ScriptableTurret> scriptableTurretTest = new List<ScriptableTurret>();
    public static ShopManager instance;
    [SerializeField] int coins;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] GameObject startWaveButton;
    [SerializeField] GameObject turretsBuyPanel;
    [SerializeField] GameObject cancelButton;
    GameObject turretSelectedToBuy;
    bool isBuyingTurrets;
    bool isInBuildArea;
    Camera mainCamera;
     
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

    void Start()
    {
        mainCamera = Camera.main;
        UpdateCoinsUI();
        StartShop();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isBuyingTurrets && isInBuildArea)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(mainCamera.transform.position.z);
            Vector3 mouseToWorldPos = mainCamera.ScreenToWorldPoint(mousePos);
            mouseToWorldPos.z = 0f;

            Instantiate(turretSelectedToBuy, mouseToWorldPos, Quaternion.identity);

            AddCoins(-turretSelectedToBuy.GetComponent<TurretCombat>().turret.turretCost);
            CancelBuy();
        }
    }

    public int GetCoins()
    {
        return coins;
    }

    public void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
        UpdateCoinsUI();
    }

    void UpdateCoinsUI()
    {
        coinsText.text = coins.ToString();
    }

    public void WaveStartButton()
    {
        ToggleShopButtons();
        WaveManager.instance.canCountDownWave = true;
    }

    public void StartShop()
    {
        ToggleShopButtons();
    }

    public void StartBuyingTurret(GameObject turretToBuy)
    {
        turretSelectedToBuy = turretToBuy;
        isBuyingTurrets = true;
        ToggleShopButtons();
        ToggleCancelButton();
    }


    public void CancelBuy()
    {
        turretSelectedToBuy = null;
        isBuyingTurrets = false;
        ToggleCancelButton();
        ToggleShopButtons();
    }

    public void SetIsInBuildArea(bool valueToSet)
    {
        isInBuildArea = valueToSet;
    }

    void ToggleShopButtons()
    {
        startWaveButton.SetActive(!startWaveButton.activeInHierarchy);
        turretsBuyPanel.SetActive(!turretsBuyPanel.activeInHierarchy);
    }

    void ToggleCancelButton()
    {
        cancelButton.SetActive(!cancelButton.activeInHierarchy);
    }
}
