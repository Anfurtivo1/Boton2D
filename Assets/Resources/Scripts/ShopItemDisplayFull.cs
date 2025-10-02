using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    GameManager gameManager;
    [Header("Data")]
    public List<ItemShop> shopItems;   // ScriptableObject assets
    
    public List<MonsterData> Shop_Available_Monsters; // ScriptableObject assets
    public List<MonsterData> Shop_Bought_Monsters;

    int Player_Money;

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
        
        FillShop();
    }

    void FillShop()
    {
        for (int i = 0; i < itemSlots.Count && i < shopItems.Count; i++)
        {
            ItemShop item = shopItems[i];
            GameObject slot = itemSlots[i];

            // Assuming each prefab has these components in children:
            TextMeshProUGUI nameText = slot.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Image iconImage = slot.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
            TextMeshProUGUI priceText = slot.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descriptionText = slot.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            // Fill with data
            nameText.text = item.itemName;
            priceText.text = "$" + item.itemPrice;
            iconImage.sprite = item.itemSprite;
            descriptionText.text = item.itemDescription;
        }
    }
}
