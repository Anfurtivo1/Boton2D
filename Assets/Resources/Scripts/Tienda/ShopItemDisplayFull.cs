using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemDisplayFull : MonoBehaviour
{
    public static ShopItemDisplayFull Instance;

    public GameObject buyingPanel;
    public string nearlyBoughtMonster;
    public GameObject buttonMonsterSelected;

    [Header("Data")]
    public List<ItemShop> shopItems;
    public List<MonsterData> Shop_Available_Monsters;
    public List<MonsterData> Shop_Bought_Monsters;//Añadir guardado con booleanos

    int Player_Money;
    public int maMoni;
    public TextMeshProUGUI maMoniText;

    int Shop_Current_House_Slots;
    MonsterData Shop_Selected_Monster;
    GameObject Shop_UI;
    List<GameObject> Shop_UI_Monster_Slots;

    public bool monster1Bought = false;
    public bool monster2Bought = false;
    public bool monster3Bought = false;
    public bool monster4Bought = false;
    public bool monster5Bought = false;
    public bool monster6Bought = false;
    public bool monster7Bought = false;

    [Header("Slots (Prefabs in Scene)")]
    public List<GameObject> itemSlots;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        maMoniText.text = "$" + GameManager.Instance.Money_Amount;
        FillShop();
    }

    private void Update()
    {
        maMoniText.text = "$" + GameManager.Instance.Money_Amount;
    }

    void FillShop()
    {
        for (int i = 0; i < itemSlots.Count && i < shopItems.Count; i++)
        {
            ItemShop item = shopItems[i];
            GameObject slot = itemSlots[i];

            TextMeshProUGUI nameText = slot.GetComponent<IAmBuyable>().itemName.GetComponent<TextMeshProUGUI>();
            Image iconImage = slot.GetComponent<IAmBuyable>().itemIcon.GetComponent<Image>();
            TextMeshProUGUI priceText = slot.GetComponent<IAmBuyable>().itemPrice.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI effectDescriptionText = slot.GetComponent<IAmBuyable>().itemEffectDescription.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI loreDescriptionText = slot.GetComponent<IAmBuyable>().itemLoreDescription.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI deathNeededText = slot.GetComponent<IAmBuyable>().itemDeathCount.GetComponent<TextMeshProUGUI>();

            nameText.text = item.itemName;
            priceText.text = "$" + item.itemPrice;
            iconImage.sprite = item.itemSprite;
            effectDescriptionText.text = item.itemEffectDescription;
            loreDescriptionText.text = item.itemLoreDescription;
            deathNeededText.text = "" + item.monsterNeededDeaths;
            slot.GetComponent<IAmBuyable>().myID = item.ID;
        }
    }

    // 🔹 Nuevo método para refrescar todos los unlocks
    public void RefreshAllUnlocks()
    {
        foreach (var slot in itemSlots)
        {
            if (slot != null)
            {
                var buyable = slot.GetComponent<IAmBuyable>();
                if (buyable != null)
                {
                    buyable.CheckUnlocks();
                }
            }
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
                break;
            }
        }

        buttonMonsterSelected.SetActive(false);
        buyingPanel.SetActive(false);
    }

    public void NopeIDontBuyIt()
    {
        nearlyBoughtMonster = "";
        buttonMonsterSelected = null;
        buyingPanel.SetActive(false);
    }
}