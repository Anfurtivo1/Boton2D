using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Objetos para ocultar/mostrar")]
    public List<GameObject> HideMenu = new List<GameObject>();
    public List<GameObject> HideShop = new List<GameObject>();
    public List<GameObject> HidePecera = new List<GameObject>();

    public List<GameObject> ShowMenu = new List<GameObject>();
    public List<GameObject> ShowShop = new List<GameObject>();
    public List<GameObject> ShowPecera = new List<GameObject>();

    [Header("Spawner")]
    public Spawner spawnerScript;  

    [Header("Cámara")]
    public Camera mainCamera;
    public float cameraMoveSpeed = 2f;

    [Header("Posiciones de cámara")]
    public Transform shopCameraPosition;
    public Transform peceraCameraPosition;

    private Coroutine cameraMoveCoroutine;

    [Header("Posiciones de cámara")]
    public GameObject canvasShop;
    public GameObject canvasPecera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if (gameObject.CompareTag("PlayButton"))
            {
                foreach (var obj in HideMenu)
                {
                    if (obj != null)
                        obj.SetActive(false);
                }

                Debug.Log("Empiezo a jugar");
                if (spawnerScript != null)
                {
                    spawnerScript.StartGame();
                }
                else
                {
                    Debug.LogWarning("No se ha asignado el Spawner en el MenuManager.");
                }
            }

            if (gameObject.CompareTag("ShopButton"))
            {
                LineBounce.lineBounceInstance.isOnMenus = true;
                canvasShop.SetActive(true);
                Debug.Log("Me voy a la tienda");
                MoveCameraTo(shopCameraPosition);
            }

            if (gameObject.CompareTag("PeceraButton"))
            {
                LineBounce.lineBounceInstance.isOnMenus = true;
                canvasPecera.SetActive(true);
                Debug.Log("Me voy a la pecera");
                MoveCameraTo(peceraCameraPosition);
            }
        }
    }

    private void MoveCameraTo(Transform targetPosition)
    {
        if (mainCamera == null || targetPosition == null) return;

        if (cameraMoveCoroutine != null)
            StopCoroutine(cameraMoveCoroutine);

        cameraMoveCoroutine = StartCoroutine(SmoothMoveCamera(targetPosition.position));
    }

    private IEnumerator SmoothMoveCamera(Vector3 targetPosition)
    {
        Vector3 startPos = mainCamera.transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * cameraMoveSpeed;
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPosition, t);
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
    }

    private void MoveCameraToOrigin()
    {
        if (mainCamera == null) return;

        if (cameraMoveCoroutine != null)
            StopCoroutine(cameraMoveCoroutine);

        // Mantiene el eje Z de la cámara
        Vector3 targetPosition = new Vector3(0f, 0f, mainCamera.transform.position.z);

        cameraMoveCoroutine = StartCoroutine(SmoothMoveCamera(targetPosition));
    }


    public void volverAtrasShop()
    {
        Player.playerInstance.canShoot = true;
        LineBounce.lineBounceInstance.isOnMenus = false;
        canvasShop.SetActive(false);
        MoveCameraToOrigin();
    }
    public void volverAtrasPecera()
    {
        Player.playerInstance.canShoot = true;
        LineBounce.lineBounceInstance.isOnMenus = false;
        canvasPecera.SetActive(false);
        MoveCameraToOrigin();
    }

}