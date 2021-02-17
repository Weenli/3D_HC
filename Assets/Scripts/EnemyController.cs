using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    [SerializeField]
    private Transform Ball;
    private float speed;
    private float  StartSpeed = 1f;
    private float DeltaSpeed = 0.4f;

    private Vector3 EnemyStartPosition;
    private void Awake() {
        LevelController.OnLevelUp += UpgradeSpeed;
        LevelController.OnStart += StartEnemy;
        EnemyStartPosition = this.transform.position;
    }

    private void StartEnemy(int obj) {
        this.transform.position = EnemyStartPosition;
        UpgradeSpeed(obj);
    }

    private void UpgradeSpeed(int i) {
        speed = StartSpeed + DeltaSpeed * i;
    }

    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(Ball.position.x, this.transform.position.y, this.transform.position.z), Time.deltaTime * speed);
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Ball") {
            this.transform.position = EnemyStartPosition;
            CubeDestroyer.OnCollisionBallAndObjects();
        }
    }
    private void OnDestroy() {
        LevelController.OnLevelUp -= UpgradeSpeed;
        LevelController.OnStart -= StartEnemy;
    }
}
