using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    // --- Singleton persistente ---
    public static LifeManager Instance;

    [Header("Vidas")]
    public int maxLives = 3;
    public int currentLives;

    [Header("Sprites de vidas")]
    public List<Image> lifeImages;
    public Sprite fullLifeSprite;
    public Sprite emptyLifeSprite;

    [Header("Pop-ups")]
    public GameObject deathPopup;   // Pop-up cuando pierdes una vida
    public GameObject adPopup;      // Pop-up para ver anuncio
    public GameObject fakeAdPopup;  // Panel de anuncio falso

    [Header("Tiempo de regeneración")]
    public float lifeCooldownHours = 8f;
    private DateTime[] lifeLostTimes;

    // ------------------------------------------------------------

    private void Awake()
    {
        // --- Singleton persistente ---
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // --- Cargar vidas guardadas ---
        currentLives = PlayerPrefs.GetInt("CurrentLives", maxLives);

        lifeLostTimes = new DateTime[maxLives];
        for (int i = 0; i < maxLives; i++)
        {
            long binaryTime = Convert.ToInt64(PlayerPrefs.GetString($"LifeLostTime{i}", "0"));
            lifeLostTimes[i] = binaryTime == 0 ? DateTime.MinValue : DateTime.FromBinary(binaryTime);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        StartCoroutine(SetupAfterStart());
    }

    private IEnumerator SetupAfterStart()
    {
        yield return null;
        ReassignSceneReferences();
        CheckLifeRegeneration();
        UpdateLifeUI();
    }

    private void Update()
    {
        CheckLifeRegeneration();
    }

    #region Escenas y referencias dinámicas

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayedReassignReferences());
    }

    private IEnumerator DelayedReassignReferences()
    {
        yield return null; // espera un frame
        ReassignSceneReferences();
    }

    private void ReassignSceneReferences()
    {
        // Limpia imágenes antiguas destruidas
        if (lifeImages != null)
            lifeImages.Clear();
        else
            lifeImages = new List<Image>();

        // --- Reasignar imágenes de vida ---
        var foundImages = FindObjectsByType<Image>(FindObjectsSortMode.None);
        foreach (var img in foundImages)
        {
            if (img != null && img.name.Contains("Life"))
                lifeImages.Add(img);
        }

        // --- Reasignar pop-ups ---
        deathPopup = FindPopupByNameOrTag("DeathPopup", "DeathPopupTag");
        adPopup = FindPopupByNameOrTag("AdPopup", "AdPopupTag");
        fakeAdPopup = FindPopupByNameOrTag("FakeAdPopup", "FakeAdPopupTag");

        Button retryButton = GameObject.Find("RetryButton")?.GetComponent<Button>();
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(OnRetryButton);
            retryButton.onClick.AddListener(VolverTiempo);
        }

        Button adButton = GameObject.Find("AdButton")?.GetComponent<Button>();
        if (adButton != null)
        {
            adButton.onClick.AddListener(OnWatchAdButton);
        }

        Button closeAdButton = GameObject.Find("CloseFakeAdButton")?.GetComponent<Button>();
        if (closeAdButton != null)
        {
            closeAdButton.onClick.AddListener(OnCloseFakeAdButton);
        }

        UpdateLifeUI();
        Debug.Log("Referencias de UI y pop-ups reasignadas correctamente.");
    }

    private GameObject FindPopupByNameOrTag(string name, string tag)
    {
        GameObject obj = GameObject.FindWithTag(tag);
        if (obj != null) return obj;

        obj = GameObject.Find(name);
        if (obj != null) return obj;

        Debug.LogWarning($"No se encontró el popup '{name}' ni tag '{tag}' en la escena.");
        return null;
    }

    #endregion

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
        {
            Debug.LogWarning("No quedan vidas, no se puede perder más.");
            return;
        }

        currentLives--;
        PlayerPrefs.SetInt("CurrentLives", currentLives);
        PlayerPrefs.SetString($"LifeLostTime{currentLives}", DateTime.Now.ToBinary().ToString());
        PlayerPrefs.Save();

        UpdateLifeUI();

        if (deathPopup != null)
        {
            deathPopup.SetActive(true);
            Debug.Log("Pop-up de muerte activado.");
        }
        else
        {
            Debug.LogWarning("El DeathPopup no está asignado tras perder una vida.");
        }
    }

    public void RecoverLife(int index = -1)
    {
        if (index == -1)
        {
            for (int i = 0; i < maxLives; i++)
            {
                if (lifeImages.Count > i && lifeImages[i].sprite == emptyLifeSprite)
                {
                    index = i;
                    break;
                }
            }
        }

        if (index == -1) return;

        currentLives = Mathf.Min(currentLives + 1, maxLives);
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
            if (i >= lifeImages.Count) continue;

            if (lifeLostTimes[i] != DateTime.MinValue)
            {
                TimeSpan elapsed = DateTime.Now - lifeLostTimes[i];
                if (elapsed.TotalHours >= lifeCooldownHours)
                    RecoverLife(i);
            }
        }
    }

    private void UpdateLifeUI()
    {
        if (lifeImages == null || lifeImages.Count == 0) return;

        for (int i = 0; i < maxLives; i++)
        {
            if (i < lifeImages.Count && lifeImages[i] != null)
            {
                lifeImages[i].sprite = (i < currentLives) ? fullLifeSprite : emptyLifeSprite;
            }
        }
    }

    #endregion

    #region Botones UI

    public void OnRetryButton()
    {
        if (deathPopup != null)
            deathPopup.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnWatchAdButton()
    {
        Debug.Log("Estoy en el anuncio");
        if (adPopup != null)
            adPopup.SetActive(false);

        if (fakeAdPopup != null)
            fakeAdPopup.SetActive(true);
    }

    public void OnCloseFakeAdButton()
    {
        Debug.Log("Estoy en el anuncio x2");

        if (LineBounce.lineBounceInstance != null)
            LineBounce.lineBounceInstance.isOnMenus = false;

        if (fakeAdPopup != null)
            fakeAdPopup.SetActive(false);

        RecoverLife();
    }

    public void OnClickEmptyLife(int lifeIndex)
    {
        if (lifeImages == null || lifeImages.Count <= lifeIndex) return;

        if (lifeImages[lifeIndex].sprite == emptyLifeSprite)
        {
            if (LineBounce.lineBounceInstance != null)
                LineBounce.lineBounceInstance.isOnMenus = true;

            if (adPopup != null)
                adPopup.SetActive(true);
        }
    }



    #endregion
}