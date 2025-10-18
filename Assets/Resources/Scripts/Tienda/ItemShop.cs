using UnityEngine;

[CreateAssetMenu(fileName = "ItemShop", menuName = "Shop/ItemShop")]
public class ItemShop : ScriptableObject
{
    public bool itemBought;
    public int itemPrice;
    public string itemName;
    public Sprite itemSprite;
    public string itemEffectDescription;
    public string itemLoreDescription;
}
