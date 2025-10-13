using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class IAmBuyable : MonoBehaviour
{
    GameManager gameManager;
    public GameObject priceTextGameObject;
    public string myPriceText;
    int myPriceInt;
    int Player_Money;
    public GameObject cellImage;

    void Start()
    {
        Int32.TryParse(priceTextGameObject.GetComponent<TextMeshProUGUI>().text, out myPriceInt);


        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
            gameManager.Money_Amount = Player_Money;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CanThePlayerBuyMe();
    }

    public void CanThePlayerBuyMe()
    {
        if(myPriceInt >= Player_Money)
        {
            cellImage.SetActive(false);
        }

        else
        {
            cellImage.SetActive(true);
        }
    }
}
