using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;
using TMPro;

public class StockSim : MonoBehaviour
{
    // STOCK FACTORS
    // I am wasting my time by typing this
    public float CurrentPrice = 1;
    // Delta means change just is case you forgot already :/
    public float DeltaPrice;
    // Trends are ordered by influence: Company Trend(CT), Stock Trend(ST), Tick Trend(TT)
    public float[] Trends = {1, 1, 1};
    // Trend Timer is only used by ST
    public float TrendTimer = 11;
    // Probability is used to decide Tick Trend and Stock Trend as they are influenced by another trend
    // 0 to 9 used by ST when CT is Bull | 10 to 19 used by ST when CT is Bear
    // 3 to 6 is used by TT when ST is Bull | 13 to 16 is used by TT when ST is Bear | 19 and 20 are used by TT when ST is Cons(olidating)
    private readonly int[] Probablity = {0, -1, -1, -1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, -1, -1, -1, -1, 0, -1, 1};
    // TPM stands for ticks per minute, Each tick the stock updates
    public int TPM = 10;
    private float TickTimer = 0;
    // Stock Factors include: Peak(Max value of stock), Risk(Multiplies losses), (Ignore the 0), Reward(Multiplies Gains)
    public float[] StockFactors = {747f, 0.97f, 0f, 1.12f};
    // TRADING FACTORS
    // Amout of shares
    public float Shares = 5;
    // Stores data about orders
    // Orders will be encoded like this (Market,Limit,Stop)(Buy,Sell)(Shares amt)(Price)
    // Here are the values in numbers   (   1  ,  2  ,  3 )( 1 , 2  )(/  xxxx  /)(xx.xx)
    // The decimal will not be included in the code but the backslashes will be.
    public string[] Orders;
    // Trend guy Dialouge B)
    private readonly string[] TGD = { };

    // UI REFRENCES
    // All the trading stuff: Current price, Delta Price, Shares, Share net worth
    public TextMeshProUGUI Ti;
    // Trend Guy B)
    public TextMeshProUGUI TG;

    // Functions

    // UIUpdate: Updates all the UI info
    private void UIUpdate() 
    {
        // Updates the Prices
        Ti.text = ("Current Price: " + CurrentPrice + "$\nChange in Price: " + DeltaPrice + "$\nShares: " + Shares + "\nShare Net worth: " + (Shares*CurrentPrice) + "$");
        // Updates trend guy
        // 
    }

    // TradeManager: Does all the trading related stuff(Further explanations are shown below)

    // Logger: Outputs all the values in the console
    private void Logger() 
    {
        // Debug Purposes only, Change values as you like
        Debug.Log("Current Price: " + CurrentPrice + "$ Delta Price: " + DeltaPrice + "$ CTrend: " + Trends[0] + " STrend: " + Trends[1] + " Timer: " + TrendTimer + " TTrend: " + Trends[2]);
        //Debug.Log(StockFactors);
    }

    // TrendManager: Does all the trend related stuff(Further explanations are shown below)
    private void TrendManager()
    {
        // This function is split into 3 sections
        // Company Trend: Checks if Peak or Valley is hit and sets accordingly (It goes dow after a peak and up after a valley)
        if (CurrentPrice >= StockFactors[0]) 
        {
            Trends[0] = -1;
        }
        else if (CurrentPrice <= 0)
        {
            Trends[0] = 1;
        }

        // Stock Trend: Checks if the Trend timer is over and then updates the trend randomly (Influnced by CT)
        if (math.floor(TrendTimer/10) == (TrendTimer - (10*math.floor(TrendTimer / 10))))
        {
            if (Trends[0] == 1)
            {
                Trends[1] = Probablity[UnityEngine.Random.Range(0, 10)];
            }
            else if (Trends[0] == -1)
            {
                Trends[1] = Probablity[UnityEngine.Random.Range(10, 20)];
            }
            TrendTimer = (10 * UnityEngine.Random.Range(3, 9));
        }

        // Tick Trend: Is random, and decides if the stock price will go up or down (Influenced by ST)
        if (Trends[1] == 1)
        {
            Trends[2] = Probablity[UnityEngine.Random.Range(3, 7)];
        }
        else if (Trends[1] == -1)
        {
            Trends[2] = Probablity[UnityEngine.Random.Range(13, 7)];
        }
        else 
        {
            Trends[2] = Probablity[UnityEngine.Random.Range(19, 21)];
        }

        // Updates the timer come on its obivous
        TrendTimer += 1;

    }

    // TickCounter: Makes all the tick the same distance apart(Time wise)
    private void TickCounter()
    {
        TickTimer += Time.deltaTime;
        if (TickTimer > 60/TPM - Time.deltaTime) 
        {
            TickTimer = 0;
            TickUpdate();
        }
    }

    // TickUpdate: Everything that happens in a tick is here
    private void TickUpdate()
    {
        // Sets DeltaPrice to a value influenced by the TT and ST
        if (Trends[1] == Trends[2]) 
        {
            DeltaPrice = UnityEngine.Random.Range(1, 11);
        }   
        else 
        {
            DeltaPrice = UnityEngine.Random.Range(1, 21);
        }

        // Multiplies DeltaPrice by the TT and then by Risk/Reward
        DeltaPrice *= Trends[2];
        DeltaPrice *= StockFactors[(int)Trends[2] + 2];

        // Makes shure that the prices stays in cents
        DeltaPrice = (math.round(DeltaPrice*100))/100;

        // Adds the Price over to the CurrentPrice
        CurrentPrice += DeltaPrice;
        if (CurrentPrice < 0)
        {
            CurrentPrice = 0;
        }

        // Debug Purposes only
        // Logger();
        // Calls Trend Manager
        TrendManager();
        // Calls UI Update
        UIUpdate();
        
    }

        // Start is called before the first frame update 
        // We know unity, we know...
        void Start()
    {
        TickUpdate();
    }

    // Update is called once per frame
    // tHaTs CrAzY!!
    void Update()
    {
        TickCounter();
    }
}
