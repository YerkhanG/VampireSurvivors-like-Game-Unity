using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUI : MonoBehaviour
{
    public GameObject powerUpPanel;
    public PowerUpCardUI powerUpCardPrefab;
    public System.Action<PowerUp> OnPowerCardSelected;
    public void HideUI()
    {
        powerUpPanel.SetActive(false);
    }
    public void ShowUI()
    {
        powerUpPanel.SetActive(true);
    }
    public void Awake()
    {
        HideUI();
    }
    public void PowerUpLevelUI(List<PowerUp> p)
    {
        foreach (Transform child in powerUpPanel.transform)
        {
            Destroy(child.gameObject);
        }
        ShowUI();
        Time.timeScale = 0f;
        foreach (PowerUp c in p)
        {
            PowerUpCardUI powerUpCardUI = Instantiate(powerUpCardPrefab, powerUpPanel.transform);
            powerUpCardUI.Initialize(c, HandleCardSelected);
        }
    }

    private void HandleCardSelected(PowerUp chosenPowerUp)
    {
        OnPowerCardSelected?.Invoke(chosenPowerUp);
        HideUI();
        Time.timeScale = 1f;
    }
}