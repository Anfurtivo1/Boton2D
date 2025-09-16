using UnityEngine;

public class Guardado : MonoBehaviour
{
    public int userFirstTimeExperience = 0;
    public float floatStart;
    public string stringStart;
    public int retrievedUserFirstTimeExperience;
    public float retrievedFloat;
    public string retrievedString;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("userFirstTimeExperience"))
        {
            retrievedUserFirstTimeExperience = PlayerPrefs.GetInt("retrievedUserFirstTimeExperience");

        }
        else
        {
            userFirstTimeExperience = 1;
            PlayerPrefs.SetInt("userFirstTimeExperience", userFirstTimeExperience);
        }

        if (PlayerPrefs.HasKey("floatStart") && PlayerPrefs.HasKey("stringStart"))
        {
            
            PlayerPrefs.SetFloat("floatStart", floatStart);
            PlayerPrefs.SetString("stringStart", stringStart);

            retrievedFloat = PlayerPrefs.GetFloat("floatStart");
            retrievedString = PlayerPrefs.GetString("retrievedString");
        }

    }
}
