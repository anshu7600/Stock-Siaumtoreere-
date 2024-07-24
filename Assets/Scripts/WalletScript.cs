using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletScript : MonoBehaviour
{
    // Come on you know this
    static float Wallet = 100.00f;
    // Profit / Loss
    public float PL = 0.00f;
    // Net worth
    public float Net = 100.00f;
    // Collective shares
    public int Shares = 0;

    // UI Refrence
    public TextMeshProUGUI Wi;


    // Start is called before the first frame update
    // No shi-
    void Start()
    {
        
    }

    // Update is called once per frame
    // wow... crazy...
    void Update()
    {
        Wi.text = ("Wallet: " + Wallet + "$\nP / L: " + PL + "$\nNet: " + Net + "$\nShares: " + Shares);
    }
}
