using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI maMoniText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maMoniText.text = "$" + GameManager.Instance.Money_Amount;
    }

    // Update is called once per frame
    void Update()
    {
        maMoniText.text = "$" + GameManager.Instance.Money_Amount;
    }
}
