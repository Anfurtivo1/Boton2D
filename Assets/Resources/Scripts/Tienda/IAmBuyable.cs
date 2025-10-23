using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IAmBuyable : MonoBehaviour
{
    GameObject shopManager;
    public string myPriceString;
    public int myPriceInt;
    public int myID;
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

        shopManager = GameObject.FindGameObjectWithTag("ShopManager");
        Invoke(nameof(TellMeYourPriceAndDeaths), 0.05f);
    }

    private void Update()
    {
    }

    public void CheckUnlocks()
    {
        if (shopManager != null)
        {
            foreach (var item in shopManager.GetComponent<ShopItemDisplayFull>().itemSlots)
            {
                enemyTypeKills = GameManager.Instance.GetMonsterKills(item.GetComponent<IAmBuyable>().myID);

                if (enemyTypeKills >= item.GetComponent<IAmBuyable>().enemyTypeKillsNeeded)
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
        }
        else
        {
            Debug.Log("No encontré el shopManager");
        }
    }

    public void TellMeYourPriceAndDeaths()
    {
        myPriceString = itemPrice.GetComponent<TextMeshProUGUI>().text.Replace("$", "").Trim();
        int.TryParse(myPriceString, out myPriceInt);
        int.TryParse(itemDeathCount.GetComponent<TextMeshProUGUI>().text, out enemyTypeKillsNeeded);
    }
}

