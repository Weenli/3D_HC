using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroyer: MonoBehaviour
{
    public static Action OnCollisionBallAndObjects;
    public static Action OnLevelCheck;
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Ball"){
            this.gameObject.SetActive(false);
            OnCollisionBallAndObjects();
            OnLevelCheck();
        }
    }
}
