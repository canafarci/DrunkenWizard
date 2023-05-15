using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
