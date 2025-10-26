using UnityEngine;
using UnityEngine.SceneManagement;

public class Guardado : MonoBehaviour
{
    public static Guardado instance;

    public int userFirstTimeExperience = 0;
    public ShopItemDisplayFull shopManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Suscribirse al evento de carga de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Evita duplicar el evento si el objeto se destruye
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Espera un frame para asegurarse de que todos los objetos se hayan instanciado
        StartCoroutine(FindShopManagerNextFrame());
    }

    private System.Collections.IEnumerator FindShopManagerNextFrame()
    {
        yield return null; // Espera 1 frame

        GameObject shopObj = GameObject.FindWithTag("ShopManager");

        if (shopObj != null)
        {
            shopManager = shopObj.GetComponent<ShopItemDisplayFull>();
            Debug.Log($"[Guardado] ShopManager encontrado en la escena '{SceneManager.GetActiveScene().name}'.");
        }
        else
        {
            Debug.LogWarning($"[Guardado] No se encontró ningún objeto con el tag 'ShopManager' en la escena '{SceneManager.GetActiveScene().name}'.");
        }
    }


    //Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    if (PlayerPrefs.HasKey("userFirstTimeExperience"))//Ya tenía datos guardados, estos datos se guardan al acabar cada run
    //    {

    //        CargarDatos();
    //        retrievedUserFirstTimeExperience = PlayerPrefs.GetInt("retrievedUserFirstTimeExperience");

    //    }

    //    if (PlayerPrefs.HasKey("floatStart") && PlayerPrefs.HasKey("stringStart"))
    //    {

    //        Guardamos desde PlayerPrefs
    //        GuardarDatos();

    //        PlayerPrefs.SetFloat("floatStart", floatStart);
    //        PlayerPrefs.SetString("stringStart", stringStart);

    //        retrievedFloat = PlayerPrefs.GetFloat("floatStart");
    //        retrievedString = PlayerPrefs.GetString("retrievedString");
    //    }


    //}

    public void CargarDatos()
    {
        // Cargamos en PlayerPrefs
        Player.playerInstance.bullet_Damage = PlayerPrefs.GetInt("bullet_Damage");
        //PlayerPrefs.GetInt("player_HP");
        GameManager.Instance.Money_Amount = PlayerPrefs.GetInt("Money_Amount");

        Player.playerInstance.player_AttackRate = PlayerPrefs.GetFloat("player_AttackRate");

        Player.playerInstance.mejoraDinero1 = PlayerPrefs.GetInt("mejoraDinero1", 0) == 1;

        Player.playerInstance.mejoraDinero2 = PlayerPrefs.GetInt("mejoraDinero2", 0) == 1;

        ShopItemDisplayFull.Instance.monster1Bought = PlayerPrefs.GetInt("monster1Bought", 0) == 1;

        ShopItemDisplayFull.Instance.monster2Bought = PlayerPrefs.GetInt("monster2Bought", 0) == 1;

        ShopItemDisplayFull.Instance.monster3Bought = PlayerPrefs.GetInt("monster3Bought", 0) == 1;

        ShopItemDisplayFull.Instance.monster4Bought = PlayerPrefs.GetInt("monster4Bought", 0) == 1;

        ShopItemDisplayFull.Instance.monster5Bought = PlayerPrefs.GetInt("monster5Bought", 0) == 1;

        ShopItemDisplayFull.Instance.monster6Bought = PlayerPrefs.GetInt("monster6Bought", 0) == 1;

        ShopItemDisplayFull.Instance.monster7Bought = PlayerPrefs.GetInt("monster7Bought", 0) == 1;

        Player.playerInstance.bullet_speed = PlayerPrefs.GetFloat("bullet_speed");

        GameManager.Instance.MonsterKills = PrefsDictionary.LoadDictionary("MonsterKills");
        //Debug.Log("Diccionario cargado desde PlayerPrefs.");

        Debug.Log("Datos cargados");
    }


    public void GuardarDatos()
    {
        // Guardamos en PlayerPrefs
        PlayerPrefs.SetInt("bullet_Damage", Player.playerInstance.bullet_Damage);
        //PlayerPrefs.SetInt("player_HP", Player.playerInstance.player_HP);
        PlayerPrefs.SetInt("Money_Amount", GameManager.Instance.Money_Amount);

        PlayerPrefs.SetFloat("player_AttackRate", Player.playerInstance.player_AttackRate);

        PlayerPrefs.SetInt("mejoraDinero1", Player.playerInstance.mejoraDinero1 ? 1 : 0);

        PlayerPrefs.SetInt("mejoraDinero2", Player.playerInstance.mejoraDinero2 ? 1 : 0);

        PlayerPrefs.SetFloat("bullet_speed", Player.playerInstance.bullet_speed);


        PlayerPrefs.SetInt("monster1Bought", ShopItemDisplayFull.Instance.monster1Bought ? 1 : 0);

        PlayerPrefs.SetInt("monster2Bought", ShopItemDisplayFull.Instance.monster2Bought ? 1 : 0);

        PlayerPrefs.SetInt("monster3Bought", ShopItemDisplayFull.Instance.monster3Bought ? 1 : 0);

        PlayerPrefs.SetInt("monster4Bought", ShopItemDisplayFull.Instance.monster4Bought ? 1 : 0);

        PlayerPrefs.SetInt("monster5Bought", ShopItemDisplayFull.Instance.monster5Bought ? 1 : 0);

        PlayerPrefs.SetInt("monster6Bought", ShopItemDisplayFull.Instance.monster6Bought ? 1 : 0);

        PlayerPrefs.SetInt("monster7Bought", ShopItemDisplayFull.Instance.monster7Bought ? 1 : 0);


        PrefsDictionary.SaveDictionary("MonsterKills", GameManager.Instance.MonsterKills);
        //Debug.Log("Diccionario guardado en PlayerPrefs.");

        userFirstTimeExperience = 1;

        PlayerPrefs.SetInt("userFirstTimeExperience", userFirstTimeExperience);

        Debug.Log("Datos guardados");


    }


}
