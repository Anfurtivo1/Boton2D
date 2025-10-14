using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private List<GameObject> HideMenu = new List<GameObject>();
    private List<GameObject> HideShop = new List<GameObject>();
    private List<GameObject> HidePecera = new List<GameObject>();

    private List<GameObject> ShowMenu = new List<GameObject>();
    private List<GameObject> ShowShop = new List<GameObject>();
    private List<GameObject> ShowPecera = new List<GameObject>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if (gameObject.tag == "PlayButton")
            {
                Debug.Log("Empiezo a jugar");
            }
            if (gameObject.tag == "ShopButton")
            {
                Debug.Log("Me voy a la tienda");
            }
            if (gameObject.tag == "PeceraButton")
            {
                Debug.Log("Me voy a la pecera");
            }
        }
    }
}
