using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour
{

    public ItemShop myShopÌtem;

    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Image itemSprite;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myShopÌtem != null)
        {
            itemName.text = myShopÌtem.itemName;
            itemPrice.text = myShopÌtem.itemPrice.ToString();
            itemSprite.sprite = myShopÌtem.itemSprite;
            itemDescription.text = myShopÌtem.itemDescription;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
