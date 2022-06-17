using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JetSystems;
using System;

public class SquadUpgrader : MonoBehaviour
{
    [Header(" Events ")]
    public static Action<int> OnSquadUpgradePurchased;

    [Header(" Managers ")]
    [SerializeField] private SquadFormation squadFormation;

    [Header(" UI Elements ")]
    [SerializeField] private Text squadLevelText;
    [SerializeField] private Text squadPriceText;
    [SerializeField] private Button squadButton;

    [Header(" Settings ")]
    [SerializeField] private int[] squadPrices;
    private int squadLevel;

    private void Awake()
    {
        LoadData();

        UIManager.onMenuSet += UpdateUI;
    }

    private void OnDestroy()
    {
        UIManager.onMenuSet -= UpdateUI;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();

        ConfigureArmy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PurchaseSquadUpgrade()
    {
        UIManager.AddCoins(-squadPrices[squadLevel]);

        if (VibrationManager.CanVibrate())
        {
            Taptic.Light();
        }

        squadLevel++;
        SaveData();

        UpdateUI();

        OnSquadUpgradePurchased?.Invoke(squadLevel);

        squadFormation.AddInitialRunners(1);    
    }

    private void ConfigureArmy()
    {
        squadFormation.AddInitialRunners(squadLevel);
    }

    private void UpdateUI()
    {
        //squadLevelText.text = squadLevel < squadPrices.Length ? "lvl " + (squadLevel + 1) : "MAX";
        //squadPriceText.text = squadLevel < squadPrices.Length ? squadPrices[squadLevel].ToString() : "";

       //    squadButton.interactable = squadLevel < squadPrices.Length - 1 && UIManager.COINS >= squadPrices[squadLevel];
    }

    private void LoadData()
    {
        squadLevel = PlayerPrefs.GetInt("SquadLevel");
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("SquadLevel", squadLevel);
    }


}
