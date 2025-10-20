using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ShopItemDisplayFull : MonoBehaviour
{
    public GameObject buyingPanel;
    public string nearlyBoughtMonster;
    public GameObject buttonMonsterSelected;

    [Header("Data")]
    public List<ItemShop> shopItems;   // ScriptableObject assets

    public List<MonsterData> Shop_Available_Monsters; // ScriptableObject assets
    public List<MonsterData> Shop_Bought_Monsters;

    int Player_Money;
    public int maMoni;
    public TextMeshProUGUI maMoniText;

    int Shop_Current_House_Slots;

    MonsterData Shop_Selected_Monster;

    GameObject Shop_UI;

    List<GameObject> Shop_UI_Monster_Slots;

    //MonsterHouseManager Shop_MonsterHouseManager;


    [Header("Slots (Prefabs in Scene)")]
    public List<GameObject> itemSlots; // The 4 prefabs in the UI

    void Start()
    {
        maMoniText.text = "$" + GameManager.Instance.Money_Amount;
        FillShop();
    }

    private void Update()
    {

    }

    void FillShop()
    {
        for (int i = 0; i < itemSlots.Count && i < shopItems.Count; i++)
        {
            ItemShop item = shopItems[i];
            GameObject slot = itemSlots[i];

            // Assuming each prefab has these components in children:
            TextMeshProUGUI nameText = slot.GetComponent<IAmBuyable>().itemName.GetComponent<TextMeshProUGUI>();
            Image iconImage = slot.GetComponent<IAmBuyable>().itemIcon.GetComponent<Image>();
            TextMeshProUGUI priceText = slot.GetComponent<IAmBuyable>().itemPrice.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI effectDescriptionText = slot.GetComponent<IAmBuyable>().itemEffectDescription.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI loreDescriptionText = slot.GetComponent<IAmBuyable>().itemLoreDescription.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI deathNeededText = slot.GetComponent<IAmBuyable>().itemDeathCount.GetComponent<TextMeshProUGUI>();

            // Fill with data
            nameText.text = item.itemName;
            priceText.text = "$" + item.itemPrice;
            iconImage.sprite = item.itemSprite;
            effectDescriptionText.text = item.itemEffectDescription;
            loreDescriptionText.text = item.itemLoreDescription;
            deathNeededText.text = "" + item.monsterNeededDeaths;
            slot.GetComponent<IAmBuyable>().myID = item.ID;
        }
    }



    public void AddButton(GameObject button)
    {
        buttonMonsterSelected = button;
    }

    public void BuyingObject(GameObject shopItem)
    {
        buyingPanel.gameObject.SetActive(true);
        buyingPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = shopItem.transform.GetChild(1).GetComponent<Image>().sprite;
        buyingPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = shopItem.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text;
        buyingPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = shopItem.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text;
        buyingPanel.transform.GetChild(0).GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = shopItem.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text;
        buyingPanel.transform.GetChild(0).GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>().text = shopItem.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text;
        nearlyBoughtMonster = shopItem.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text;
    }

    public void ConfirmTheChoice()
    {
        for (int i = 0; i < Shop_Available_Monsters.Count; i++)
        {
            if (Shop_Available_Monsters[i].Monster_Name == nearlyBoughtMonster)
            {
                Shop_Bought_Monsters.Add(Shop_Available_Monsters[i]);
                Shop_Available_Monsters.Remove(Shop_Available_Monsters[i]);
            }
        }

        Destroy(buttonMonsterSelected);
        buyingPanel.SetActive(false);
    }

    public void NopeIDontBuyIt()
    {
        nearlyBoughtMonster = "";
        buttonMonsterSelected = null;
        buyingPanel.SetActive(false);
    }
}
