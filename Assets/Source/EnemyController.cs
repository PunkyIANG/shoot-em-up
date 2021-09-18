using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyPhase{
    Movement,
    Attack,
    DefaultIdle
}

public class EnemyController : MonoBehaviour
{
    PlayerController playerObject;
    EnemyPhase enemyPhase;
    public GameObject enemyBulletPrefab;

    void Start()
    {
        playerObject = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        
    }
}
