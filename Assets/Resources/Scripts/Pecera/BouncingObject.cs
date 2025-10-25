using UnityEngine;
using UnityEngine.UI;

public class BouncingObject : MonoBehaviour
{
    //public float speed;
    //private Vector2 direction;

    //// Límites de rebote
    //public float minX;
    //public float maxX;
    //public float minY;
    //public float maxY;

    //private float halfWidth;
    //private float halfHeight;

    public bool isActive = true;
    public MonsterHouseManager monsterHouse;
    public string enemyName;

    void Start()
    {
        //// Dirección inicial aleatoria
        //direction = Random.insideUnitCircle.normalized;

        //// Obtener tamaño del sprite
        //SpriteRenderer sr = GetComponent<SpriteRenderer>();
        //if (sr != null)
        //{
        //    Vector2 size = sr.bounds.size;
        //    halfWidth = size.x / 2f;
        //    halfHeight = size.y / 2f;
        //}
    }

    public void ActivacionManager()
    {
        if (isActive)
        {
            DesactivarMonstruo();//quitar mejoras
            isActive = false;
            monsterHouse.MejorasManager(enemyName,true);

        }
        else
        {
            ActivarMonstruo();//poner mejoras
            isActive = true;
            monsterHouse.MejorasManager(enemyName, false);
        }
    }

    void ActivarMonstruo()
    {
        if (TryGetComponent<SpriteRenderer>(out var sr))
        {
            Color c = sr.color;
            c.a = 1f;
            sr.color = c;
        }
        else if (TryGetComponent<Image>(out var img))
        {
            Color c = img.color;
            c.a = 1f;
            img.color = c;
        }
    }
    void DesactivarMonstruo()
    {
        if (TryGetComponent<SpriteRenderer>(out var sr))
        {
            Color c = sr.color;
            c.a = 0.5f;
            sr.color = c;
        }
        else if (TryGetComponent<Image>(out var img))
        {
            Color c = img.color;
            c.a = 0.5f;
            img.color = c;
        }
    }
    void Update()
    {
        //Vector3 pos = transform.position;
        //pos += (Vector3)(direction * speed * Time.deltaTime);

        //// Rebote horizontal
        //if (pos.x - halfWidth < minX || pos.x + halfWidth > maxX)
        //{
        //    direction.x = -direction.x;
        //    pos.x = Mathf.Clamp(pos.x, minX + halfWidth, maxX - halfWidth);
        //}

        //// Rebote vertical
        //if (pos.y - halfHeight < minY || pos.y + halfHeight > maxY)
        //{
        //    direction.y = -direction.y;
        //    pos.y = Mathf.Clamp(pos.y, minY + halfHeight, maxY - halfHeight);
        //}

        //transform.position = pos;
    }
}
