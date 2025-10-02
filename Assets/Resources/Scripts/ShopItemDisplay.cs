using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour
{

    public ItemShop myShopItem;

    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Image itemSprite;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myShopItem != null)
        {
            
            itemName.text = myShopItem.itemName;
            itemPrice.text = myShopItem.itemPrice.ToString();
            itemSprite.sprite = myShopItem.itemSprite;
            itemDescription.text = myShopItem.itemDescription;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
