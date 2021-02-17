using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInput : MonoBehaviour {
    [SerializeField]
    private Rigidbody Ball;
    [SerializeField]
    private Transform DirectionArrow;
    private RaycastHit Hit;
    private float Speed = 5.0f;
    private Vector3 CubeStartPosition;
    private Vector3 BallStartPosition;
    private bool CanBeControlled;
    private Vector2 StartPos;
    void Start() {
        CanBeControlled = true;
        CubeStartPosition = this.transform.position;
        BallStartPosition = Ball.transform.position;
        CubeDestroyer.OnCollisionBallAndObjects += CollisionBallAndObjects;
        LevelController.OnStart += StartForPlayer;
    }


    private void StartForPlayer(int i) {
        CollisionBallAndObjects();
    }
    private void CollisionBallAndObjects() {
        Ball.velocity = Vector3.zero;
        Ball.position = BallStartPosition;
        this.transform.position = CubeStartPosition;
        CanBeControlled = true;
    }

    private void OnMouseDown() {
        if (CanBeControlled == true) {
            DirectionArrow.gameObject.SetActive(true);
        }
    }

    private void OnMouseDrag() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit)) {
            transform.position = new Vector3(Mathf.Clamp(Hit.point.x, -5f, 5f), 2, Mathf.Clamp(Hit.point.z, -15, -10));
            PlayerRotation();
        }
    }

    private void OnMouseUp() {
        if (CanBeControlled) {
            Vector3 direction = new Vector3(this.transform.position.x - Ball.transform.position.x, 0, this.transform.position.z - Ball.transform.position.z);
            float ForceScaler = Vector3.Distance(Ball.transform.position, this.transform.position);
            Ball.velocity = direction.normalized * Speed * ForceScaler;
            CanBeControlled = false;
        }
        transform.position = CubeStartPosition;
        transform.rotation = Quaternion.identity;
        DirectionArrow.gameObject.SetActive(false);
    }

    private void PlayerRotation() {
        double deltaZ = (transform.position.z - Ball.position.z);
        double hypotenuse = Math.Sqrt((Math.Pow((transform.position.z - Ball.position.z), 2) + Math.Pow(this.transform.position.x, 2)));
        float AngleForCube = (float)Math.Acos(deltaZ / hypotenuse);
        transform.rotation = Quaternion.Euler(0, AngleForCube * Mathf.Sign(transform.position.x) * 180 / (float)Math.PI, 0);
        float ArrowScaler = Vector3.Distance(Ball.transform.position, this.transform.position);
        DirectionArrow.localScale = new Vector3(Mathf.Clamp(ArrowScaler * 0.02f, 0.11f, 0.2f), 0.1f, 0.1f);
    }
}
