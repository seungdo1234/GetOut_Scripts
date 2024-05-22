using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUIHandler : MonoBehaviour
{
    [SerializeField] private Sprite[] heartSprites;
    [SerializeField] private Image[] hearts;

    private HealthSystem playerHealthSystem;

    private void Start()
    {
        playerHealthSystem = FlightDataManager.Instance.PlayerFlightStat.GetComponent<HealthSystem>();

    }

    public void UpdateHeart()
    {
        int idx = 0, health = (int)playerHealthSystem.CurHealth;
        
        for (;idx < health / 2; idx++)
        {
            hearts[idx].sprite = heartSprites[(int)EHeartType.Full];
        }
        
        if (health % 2 == 1)
        {
            hearts[idx].sprite = heartSprites[(int)EHeartType.Half];
            idx++;
        }
        
        for (;idx < hearts.Length; idx++)
        {
            hearts[idx].sprite = heartSprites[(int)EHeartType.Empty];
        }
    }
}
