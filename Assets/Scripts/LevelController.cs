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
    public int level { get; private set; }

    private void Start() {
        CubeForDestroy.LevelCheck += CheckLevelComplete;
        level = 1;
    }

    private void CheckLevelComplete() {
        int Count = 0;
        foreach(GameObject cube in Cubes) {
            if (!cube.activeSelf) {
                Count++;
            }
        }
        if (Count == Cubes.Count) {
            level++;
            OnLevelUp(level);
            foreach(GameObject cube in Cubes) {
                cube.SetActive(true);
            }
        }
    }
    public void StartGame() {
        if (level != 1) {
            OnStart(level);
        }
    }
}
