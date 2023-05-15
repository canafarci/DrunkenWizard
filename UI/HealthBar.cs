using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetHealthBar(float target)
    {
        _image.fillAmount = target;
    }
}
