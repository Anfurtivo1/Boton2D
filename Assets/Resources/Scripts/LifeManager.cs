using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [Header("Vidas")]
    public int maxLives = 3;
    public int currentLives;

    [Header("Sprites de vidas")]
    public List<Image> lifeImages; 
    public Sprite fullLifeSprite;
    public Sprite emptyLifeSprite;

    [Header("Pop-ups")]
    public GameObject deathPopup;       //Pop-up cuando pierdes una vida
    public GameObject adPopup;          //Pop-up para ver anuncio
    public GameObject fakeAdPopup;      //Panel de anuncio falso

    [Header("Tiempo de regeneración")]
    public float lifeCooldownHours = 8f;
    private DateTime[] lifeLostTimes;

    private void Awake()
    {
        //Cargar vidas guardadas
        currentLives = PlayerPrefs.GetInt("CurrentLives", maxLives);//Al cambiar el limite de vidas, esto peta

        lifeLostTimes = new DateTime[maxLives];
        for (int i = 0; i < maxLives; i++)
        {
            long binaryTime = Convert.ToInt64(PlayerPrefs.GetString($"LifeLostTime{i}", "0"));
            lifeLostTimes[i] = binaryTime == 0 ? DateTime.MinValue : DateTime.FromBinary(binaryTime);
        }

        UpdateLifeUI();
    }

    private void Start()
    {
        CheckLifeRegeneration();
    }

    private void Update()
    {
        ////Cargar vidas guardadas
        //currentLives = PlayerPrefs.GetInt("CurrentLives", maxLives);

        //lifeLostTimes = new DateTime[maxLives];
        //for (int i = 0; i < maxLives; i++)
        //{
        //    long binaryTime = Convert.ToInt64(PlayerPrefs.GetString($"LifeLostTime{i}", "0"));
        //    lifeLostTimes[i] = binaryTime == 0 ? DateTime.MinValue : DateTime.FromBinary(binaryTime);
        //}

        //UpdateLifeUI();

        CheckLifeRegeneration();
    }

    #region Lógica de vidas

    public bool HasLives()
    {
        return currentLives > 0;
    }

    public void VolverTiempo()
    {
        Time.timeScale = 1f;
    }

    public void LoseLife()
    {
        if (currentLives <= 0)
            return;

        currentLives--;
        UpdateLifeUI();

        lifeLostTimes[currentLives] = DateTime.Now;
        PlayerPrefs.SetString($"LifeLostTime{currentLives}", DateTime.Now.ToBinary().ToString());

        PlayerPrefs.SetInt("CurrentLives", currentLives);
        PlayerPrefs.Save();

        if (deathPopup != null)
            deathPopup.SetActive(true);
    }

    public void RecoverLife(int index = -1)
    {
        if (index == -1)
        {
            for (int i = 0; i < maxLives; i++)
            {
                if (lifeImages[i].sprite == emptyLifeSprite)
                {
                    index = i;
                    break;
                }
            }
        }

        if (index == -1) return; 

        currentLives++;
        lifeLostTimes[index] = DateTime.MinValue; 
        PlayerPrefs.SetInt("CurrentLives", currentLives);
        PlayerPrefs.SetString($"LifeLostTime{index}", "0");
        PlayerPrefs.Save();

        UpdateLifeUI();
    }

    private void CheckLifeRegeneration()
    {
        for (int i = 0; i < maxLives; i++)
        {
            if (lifeImages[i].sprite == emptyLifeSprite && lifeLostTimes[i] != DateTime.MinValue)
            {
                TimeSpan elapsed = DateTime.Now - lifeLostTimes[i];
                if (elapsed.TotalHours >= lifeCooldownHours)
                {
                    RecoverLife(i);
                }
            }
        }
    }

    private void UpdateLifeUI()
    {
        for (int i = 0; i < maxLives; i++)
        {
            if (i < currentLives)
                lifeImages[i].sprite = fullLifeSprite;
            else
                lifeImages[i].sprite = emptyLifeSprite;
        }
    }

    #endregion

    #region Botones UI

    //Llamar desde botón "Reintentar" en deathPopup
    public void OnRetryButton()
    {
        if (deathPopup != null)
            deathPopup.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Llamar desde botón "Ver anuncio" en adPopup
    public void OnWatchAdButton()
    {
        Debug.Log("Estoy en el anuncio");
        if (adPopup != null)
            adPopup.SetActive(false);

        if (fakeAdPopup != null)
            fakeAdPopup.SetActive(true);
    }

    //Llamar desde botón "Cerrar anuncio" en fakeAdPopup
    public void OnCloseFakeAdButton()//Vuelva a disparar
    {
        Debug.Log("Estoy en el anuncio x2");
        LineBounce.lineBounceInstance.isOnMenus = false;
        if (fakeAdPopup != null)
            fakeAdPopup.SetActive(false);

        RecoverLife();
    }

    public void OnClickEmptyLife(int lifeIndex)//Que no pueda disparar
    {
        if (lifeImages[lifeIndex].sprite == emptyLifeSprite)
        {
            LineBounce.lineBounceInstance.isOnMenus = true;
            if (adPopup != null)
                adPopup.SetActive(true);
        }
    }

    #endregion
}
