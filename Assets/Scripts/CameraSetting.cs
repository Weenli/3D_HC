using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    private void Start() {
        Camera.main.fieldOfView = (int)30 * Screen.height / Screen.width;
    }
}
