using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime;
    
    bool isInited = false;
    private Vector2 direction;
    private static Stopwatch bulletStopwatch = new Stopwatch();
    private float startTimeStamp;

    private void Start()
    {
        if (!bulletStopwatch.IsRunning)
            bulletStopwatch.Start();

        startTimeStamp = (float)bulletStopwatch.ElapsedTicks / Stopwatch.Frequency;
    }

    public void InitBullet(Vector2 direction)
    {
        isInited = true;
        this.direction = direction * speed;
    }

    private void Update()
    {
        if (isInited)
            transform.Translate(direction * Time.deltaTime);

        if (startTimeStamp + lifetime < (float) bulletStopwatch.ElapsedTicks / Stopwatch.Frequency)
            Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
