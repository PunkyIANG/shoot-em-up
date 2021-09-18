using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject playerBulletPrefab;

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        Vector2 deltaDirection = new Vector2(horizontal, vertical) * speed * Time.deltaTime;

        transform.Translate(deltaDirection);

        if (Input.GetKey(KeyCode.Space))
            
    }


}
