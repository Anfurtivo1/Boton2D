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
    public int enemyTypeKills;
    public int enemyTypeKillsNeeded;
    public GameObject itemIcon;
    public GameObject bG_Image_Front;
    public GameObject bG_Image_Back;
    public GameObject itemName;
    public GameObject itemPrice;
    public GameObject itemEffectDescription;
    public GameObject itemLoreDescription;
    public GameObject itemDeathCount;
    public Sprite buyableUp;
    public Sprite buyableDown;

    void Start()
    {
        // Eliminar el símbolo $ y cualquier espacio
        Invoke(nameof(TellMeYourPriceAndDeaths), 0.05f);


        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
            gameManager.Money_Amount = Player_Money;
        }

        else
        {
            Player_Money = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopItemDisplayFull>().maMoni;
            enemyTypeKills = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopItemDisplayFull>().maMoni;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CanThePlayerBuyMe()
    {
        Player_Money = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopItemDisplayFull>().maMoni;
        enemyTypeKills = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopItemDisplayFull>().maMoni;

        if (enemyTypeKills >= enemyTypeKillsNeeded)
        {
            bG_Image_Back.GetComponent<Image>().sprite = buyableUp;
            bG_Image_Front.GetComponent<Image>().sprite = buyableDown;

            if (Player_Money >= myPriceInt)
            {
                itemIcon.GetComponent<Image>().color = Color.white;
                gameObject.transform.GetChild(0).GetComponent<Button>().enabled = true;
            }

            else
            {
                itemIcon.GetComponent<Image>().color = Color.black;
                gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
            }
        }
    }

    public void TellMeYourPriceAndDeaths()
    {
        myPriceString = itemPrice.GetComponent<TextMeshProUGUI>().text.Replace("$", "").Trim();
        int.TryParse(myPriceString, out myPriceInt);
        int.TryParse(itemDeathCount.GetComponent<TextMeshProUGUI>().text, out enemyTypeKillsNeeded);
        CanThePlayerBuyMe();
    }
}

