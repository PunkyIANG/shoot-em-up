using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject playerBulletPrefab;
    public float cooldown;
    
    private readonly Stopwatch cooldownStopwatch = new Stopwatch();
    private float lastShotTimestamp;


    private void Start()
    {
        cooldownStopwatch.Start();
        lastShotTimestamp = cooldownStopwatch.ElapsedTicks;
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var deltaDirection = speed * Time.deltaTime * new Vector2(horizontal, vertical);

        transform.Translate(deltaDirection);

        if (Input.GetKey(KeyCode.Space) && 
            (float)cooldownStopwatch.ElapsedTicks / Stopwatch.Frequency > lastShotTimestamp + cooldown)
        {
            lastShotTimestamp = (float)cooldownStopwatch.ElapsedTicks / Stopwatch.Frequency;
            
            var bullet = Instantiate(playerBulletPrefab);
            bullet.transform.position = transform.position;

            // pls find a better way to get instantiated gameObject scripts
            var bulletScript = bullet.GetComponent<Bullet>();
            
            bulletScript.InitBullet(Vector2.up);
        }
    }
}
