using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IAmBuyable : MonoBehaviour
{
    GameManager gameManager;
    public string myPriceString;
    public int myPriceInt;
    public int Player_Money;
    public GameObject itemIcon;
    public GameObject bG_Image;
    public GameObject itemName;
    public GameObject itemPrice;
    public GameObject itemEffectDescription;
    public GameObject itemLoreDescription;

    void Start()
    {
        // Eliminar el símbolo $ y cualquier espacio
        Invoke(nameof(TellMeYourPrice), 0.1f);

        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
            gameManager.Money_Amount = Player_Money;
        }

        else
        {
            Player_Money = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopItemDisplayFull>().maMoni;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CanThePlayerBuyMe();
    }

    public void CanThePlayerBuyMe()
    {
        Player_Money = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopItemDisplayFull>().maMoni;

        if (myPriceInt >= Player_Money)
        {
            itemIcon.GetComponent<Image>().color = Color.black;
        }

        else
        {
            itemIcon.GetComponent<Image>().color = Color.white;
        }
    }

    public void TellMeYourPrice()
    {
        myPriceString = itemPrice.GetComponent<TextMeshProUGUI>().text.Replace("$", "").Trim();
        int.TryParse(myPriceString, out myPriceInt);
    }
}
