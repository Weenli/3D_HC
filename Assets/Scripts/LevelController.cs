using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Cubes;
    public static Action<int> OnLevelUp;
    public static Action<int> OnStart;
    private int level;
    private void Awake() {
        Camera.main.fieldOfView = (int)30 * Screen.height / Screen.width; 
    }

    private void Start() {
        CubeDestroyer.OnLevelCheck += LevelCheck;
        level = 1;
    }

    private void LevelCheck() {
        int count = 0;
        foreach(GameObject cube in Cubes) {
            if (!cube.activeSelf) {
                count++;
            }
        }
        if (count == Cubes.Count) {
            level++;
            OnLevelUp(level);
            foreach(GameObject cube in Cubes) {
                cube.SetActive(true);
            }
        }
    }
    public void StartGame() {
            OnStart(level);
    }
}
