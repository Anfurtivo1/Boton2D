using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ShopItemDisplayFull : MonoBehaviour
{
    GameManager gameManager;
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
        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
            gameManager.Money_Amount = Player_Money;
        }

        else
            Player_Money = maMoni;

        FillShop();
        InvokeRepeating(nameof(MaMoniGrousEvriSecond), 0, 0.01f);
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
            TextMeshProUGUI descriptionText = slot.GetComponent<IAmBuyable>().itemDescription.GetComponent<TextMeshProUGUI>();

            // Fill with data
            nameText.text = item.itemName;
            priceText.text = "$" + item.itemPrice;
            iconImage.sprite = item.itemSprite;
            descriptionText.text = item.itemDescription;
        }

        
    }

    public void BuyObject()
    {

    }

    public void MaMoniGrousEvriSecond()
    {
        maMoni++;
        maMoniText.text = "$" + maMoni.ToString();
    }

}
