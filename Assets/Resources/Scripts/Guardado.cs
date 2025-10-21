using UnityEngine;

public class Guardado : MonoBehaviour
{
    public static Guardado instance;

    public int userFirstTimeExperience = 0;
    //public float floatStart;
    //public string stringStart;
    //public int retrievedUserFirstTimeExperience;
    //public float retrievedFloat;
    //public string retrievedString;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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

        GameManager.Instance.MonsterKills = PrefsDictionary.LoadDictionary("MonsterKills");
        //Debug.Log("Diccionario cargado desde PlayerPrefs.");

        Debug.Log("Datos cargados");
    }


    public void GuardarDatos()
    {
        // Guardamos en PlayerPrefs
        PlayerPrefs.SetInt("bullet_Damage",Player.playerInstance.bullet_Damage);
        //PlayerPrefs.SetInt("player_HP", Player.playerInstance.player_HP);
        PlayerPrefs.SetInt("Money_Amount", GameManager.Instance.Money_Amount);

        PlayerPrefs.SetFloat("player_AttackRate", Player.playerInstance.player_AttackRate);

        PrefsDictionary.SaveDictionary("MonsterKills", GameManager.Instance.MonsterKills);
        //Debug.Log("Diccionario guardado en PlayerPrefs.");

        userFirstTimeExperience = 1;

        PlayerPrefs.SetInt("userFirstTimeExperience", userFirstTimeExperience);

        Debug.Log("Datos guardados");


    }


}
