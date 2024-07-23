using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UImaster : MonoBehaviour
{
    public TextMeshProUGUI CurrentPrice;
    public StockSim Stock;
    public float cp;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // cp = FindObjectOfType<StockSim>().CurrentPrice;
        // Debug.Log(cp);
    }
}
