using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float speed;
    public bool doMove = true;
    public float rectPadding = 2f;

    private static PlayerController player;
    private static readonly Stopwatch stopwatch = new Stopwatch();
    private static bool isScreenRectInited = false; 
    private static Rect screenRect;
    private static Rect dirBounds = new Rect(-1f, -1f, 2f, 2f);

    private Vector2 currentDirection;
    private float timeTillDirSwitch;
    private float lastDirSwitchTime;
    
    private void Start()
    {
        // shitty init w/ bool because unity doesn't like static stuff
        if (!isScreenRectInited)
        {
            var camera = Camera.main;

            var position = camera.transform.position;
            var orthographicSize = camera.orthographicSize;
            var aspect = camera.aspect;
            
            screenRect = new Rect
            {
                xMin = position.x - orthographicSize + rectPadding,
                xMax = position.x + orthographicSize - rectPadding,
                yMin = position.y - orthographicSize * aspect + rectPadding,
                yMax = position.y + orthographicSize * aspect - rectPadding
            };
            
            isScreenRectInited = true;
            
            print(screenRect);
        }
        
        if (!stopwatch.IsRunning)
            stopwatch.Start();

        if (player == null)
            player = FindObjectOfType<PlayerController>();
        
        SwitchDirection();
    }

    private void Update()
    {
        // if the enemy goes out of bounds then redirect its path
        if (!screenRect.Contains(transform.position))
        {
            var pos = transform.position;

            var minX = pos.x < screenRect.xMin ? 0 : dirBounds.xMin;
            var maxX = pos.x > screenRect.xMax ? 0 : dirBounds.xMax;
            var minY = pos.y < screenRect.yMin ? 0 : dirBounds.yMin;
            var maxY = pos.y > screenRect.yMax ? 0 : dirBounds.yMax;
            
            SwitchDirection(minX, maxX, minY, maxY);
        } 
        else if (lastDirSwitchTime + timeTillDirSwitch < (float)stopwatch.ElapsedTicks / Stopwatch.Frequency)
            SwitchDirection();
        
        if (doMove)
            transform.Translate(speed * Time.deltaTime * currentDirection);
    }

    private void SwitchDirection(float minX = 1f, float maxX = 1f, float minY = -1f, float maxY = 1f)
    {
        var horizontal = Random.Range(minX, maxX);
        var vertical   = Random.Range(minY, maxY);

        currentDirection = new Vector2(horizontal, vertical).normalized;

        timeTillDirSwitch = Random.Range(0.5f, 2f);
        lastDirSwitchTime = (float)stopwatch.ElapsedTicks / Stopwatch.Frequency;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
