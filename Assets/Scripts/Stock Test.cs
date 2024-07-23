using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockTest : MonoBehaviour
{
    public float DeltaP;
    public float CP = 20;
    float CurrentTrend;
    float trend;
    public string Trend;
    public float TrendTimer = 0;
    public float TrendTime;
    private float TickTimer;
    public int tpm = 10;
    public float Risk = 0.97f;
    public float Reward = 1.24f;
    private readonly int[] bull = {1, 1, 1, -1};
    private readonly int[] bear = { -1, -1, -1, 1 };
    public float Peak;
    public float Valley;
    public float CompanyTrend;


    private void UpdateTrend()
    {
        trend = Random.Range(-1, 2);
        if (trend == 0)
        {
            Trend = "Cons";
        }
        else if (trend == -1) 
        {
            Trend = "Bull";
        }
        else if (trend == 1) 
        {
            Trend = "Bear";
        }

    }

    private void TrendCounter() 
    {
        if (TrendTimer == TrendTime) 
        {
            TrendTime = Random.Range(3, 9);
            TrendTimer = 0;
            UpdateTrend();
        }
    }

    private void TickUpdate()
    {
        TrendTimer += 1;

        if (Trend == "Bull")
        {
            CurrentTrend = bull[Random.Range(0, 5)];
        }
        else if (Trend == "Bear")
        {
            CurrentTrend = bear[Random.Range(0, 5)];
        }
        else 
        {
            CurrentTrend = Random.Range(0, 2);
            if (CurrentTrend == 0)
            {
                CurrentTrend = -1;
            }
        }

        if (CurrentTrend == trend)
        {
            DeltaP = Random.Range(1, 21);
        }
        else 
        {
            DeltaP = Random.Range(1, 11);
        }

        DeltaP = DeltaP * CurrentTrend;

        if (DeltaP < 0)
        {
            DeltaP = DeltaP * Risk;
        }
        else 
        {
            DeltaP = DeltaP * Reward;
        }

        CP += DeltaP;
        if (CP < 0)
        {
            CP = 0;
        }

        TrendCounter();
        Debug.Log("Cp: " + CP + "$ DeltaP: " + DeltaP + "$ Trend: " + Trend);
    }


    private void TickCounter() 
    {
        TickTimer += Time.deltaTime;
        if (TickTimer > (60/tpm-Time.deltaTime))
        { 
            TickTimer = 0;
            TickUpdate();
        }
    }
    
    void Start()
    {
        Debug.Log("Its a brand new day at Wall Street!");
        TrendTime = Random.Range(3, 16);
        TrendTimer = 0;
        UpdateTrend();
        Risk = 0.97f;
        Reward = 1.24f;
    }


    // Update is called once per frame
    void Update()
    {
        TickCounter();
    }
}
