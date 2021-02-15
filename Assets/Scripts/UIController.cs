using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text LevelText;
    private void Start() {
        LevelController.OnLevelUp += LevelTextUpdate;
    }

    private void LevelTextUpdate(int level) {
        LevelText.text = "Level:" + level.ToString();
    }
    public void Exit() {
        Application.Quit();
    }
    public void Pause(bool IsPaused) {
        Time.timeScale = (IsPaused) ? 0.0f : 1.0f ;
    }
}
