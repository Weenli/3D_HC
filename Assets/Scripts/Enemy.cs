using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform Ball;
    private float speed;
    private float StartSpeed = 0.5f;
    private float DeltaSpeed = 0.25f;

    private Vector3 EnemyStartPosition;
    private void Start() {
        EnemyStartPosition = this.transform.position;
        LevelController.OnLevelUp += UpgradeSpeed;
        LevelController.OnStart += StartEnemy;
        speed = StartSpeed; 
    }

    private void StartEnemy(int obj) {
        this.transform.position = EnemyStartPosition;
        UpgradeSpeed(obj);
    }

    private void UpgradeSpeed(int i) {
        speed += StartSpeed += DeltaSpeed * i;
    }

    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(Ball.position.x, this.transform.position.y, this.transform.position.z), Time.deltaTime * speed);
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Ball") {
            this.transform.position = EnemyStartPosition;
            CubeForDestroy.OnCollisionBallAndObjects();
        }
    }
}
