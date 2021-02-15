using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Ball;
    [SerializeField]
    private Transform Arrow;
    private RaycastHit Hit;
    private float Speed = 5.0f;
    private Vector3 CubeStartPosition;
    private Vector3 BallStartPosition;
    private bool CanBeControlled;
    private Vector2 StartPos;
      void Start()
      {
        CanBeControlled = true;
        CubeStartPosition = this.transform.position;
        BallStartPosition = Ball.transform.position;
        CubeForDestroy.OnCollisionBallAndObjects += BallHit;
        LevelController.OnStart += StartForPlayer;
    }


    private void StartForPlayer(int i) {
        BallHit();
    }
    private void BallHit() {
        Ball.velocity = Vector3.zero;
        Ball.position = BallStartPosition;
        this.transform.position = CubeStartPosition;
        CanBeControlled = true;
    }
    
     private void OnMouseDown() {
         if (CanBeControlled == true) {
             Arrow.gameObject.SetActive(true);
         }
     }
    
     private void OnMouseDrag() {
    
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit)){
             transform.position = new Vector3(Mathf.Clamp(Hit.point.x, -2f,2f),1, Mathf.Clamp(Hit.point.z, -10, -7));
             double AngleForCube = Math.Acos((transform.position.z - Ball.position.z) / Math.Sqrt((Math.Pow((transform.position.z - Ball.position.z), 2) + Math.Pow(this.transform.position.x , 2))));
             transform.rotation = Quaternion.EulerAngles(0,(float)AngleForCube * Mathf.Sign(transform.position.x),0);
             float ArrowScaler = Vector3.Distance(Ball.transform.position, this.transform.position);
             Arrow.transform.localScale = new Vector3(Mathf.Clamp(ArrowScaler *0.05f, 0.15f, 0.35f), 0.1f, 0.1f);
         }
     }
    
     private void OnMouseUp() {
         if (CanBeControlled) {
             Vector3 heading = this.transform.position - Ball.transform.position;
             float ForceScaler = Vector3.Distance(Ball.transform.position, this.transform.position);
             Ball.velocity = heading.normalized * Speed * ForceScaler;
             CanBeControlled = false;
         }
         Arrow.gameObject.SetActive(false);
         this.transform.position = CubeStartPosition;
         this.transform.rotation = Quaternion.identity;
     }

}
