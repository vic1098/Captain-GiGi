using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{

    public int itemID;
    public TMP_Text coinsPriceText;
    public TMP_Text diamondPriceText;
    public GameObject shopUI;

    // Update is called once per frame
    void Update()
    {
        // Update the price on the item
        coinsPriceText.text = "x " + shopUI.GetComponent<ShopUI>().shopItems[2,itemID].ToString();
        diamondPriceText.text = "x " + shopUI.GetComponent<ShopUI>().shopItems[3,itemID].ToString();
        
        
    }
}
