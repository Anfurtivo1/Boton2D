using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class MonsterFadeIn : MonoBehaviour
{
    [Header("Fade")]
    public float fadeDuration = 1f; // Duración del fade in

    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    //private bool fading = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        // Empieza transparente
        Color c = spriteRenderer.color;
        c.a = 0f;
        spriteRenderer.color = c;

        // Desactiva el collider
        if (col != null)
            col.enabled = false;

        // Si el enemigo tiene una IA o script de movimiento, desactívalo temporalmente (opcional)
        ToggleMovement(false);

        //fading = true;
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);

            Color c = spriteRenderer.color;
            c.a = alpha;
            spriteRenderer.color = c;

            yield return null;
        }

        // Reactiva el collider y el movimiento
        if (col != null)
            col.enabled = true;

        ToggleMovement(true);
        //fading = false;
    }

    private void ToggleMovement(bool enable)
    {
        // Si el enemigo tiene un script llamado "EnemyController" o similar, lo habilitamos/deshabilitamos.
        var controller = GetComponent<EnemyController>();
        if (controller != null)
            controller.enabled = enable;
    }
}
