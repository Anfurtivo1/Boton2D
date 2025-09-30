using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour
{

    public ItemShop myShop�tem;

    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Image itemSprite;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myShop�tem != null)
        {
            itemName.text = myShop�tem.itemName;
            itemPrice.text = myShop�tem.itemPrice.ToString();
            itemSprite.sprite = myShop�tem.itemSprite;
            itemDescription.text = myShop�tem.itemDescription;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
