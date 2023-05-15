using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotter : MonoBehaviour
{
    static int ssIndex;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ScreenCapture.CaptureScreenshot($"{ssIndex}.png");
            ssIndex++;
        }
    }
}
