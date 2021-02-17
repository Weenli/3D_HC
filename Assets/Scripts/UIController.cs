using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField]
    private Text LevelText;
    private void Awake() {
        LevelController.OnLevelUp += UpdateLevelText;
        LevelController.OnStart += UpdateLevelText;
    }
    public void Exit() {
        Application.Quit();
    }
    public void Pause(bool IsPaused) {
        Time.timeScale = (IsPaused) ? 0.0f : 1.0f;
    }
    private void UpdateLevelText(int level) {
        LevelText.text = "Lvl: " + level.ToString();
    }
    private void OnDestroy() {
        LevelController.OnStart -= UpdateLevelText;
        LevelController.OnLevelUp -= UpdateLevelText;
    }

}
