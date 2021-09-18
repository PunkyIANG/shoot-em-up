using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour
{
    bool isInited = false;
    Vector2 direction;
    public float speed = 5f;

    void InitBullet(Vector2 direction) {
        this.direction = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInited)
            transform.Translate(direction * Time.deltaTime);
    }


}
