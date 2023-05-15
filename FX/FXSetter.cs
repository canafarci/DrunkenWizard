using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSetter : MonoBehaviour
{
    public FXObjects FXs;

    [SerializeField] AudioSource _bg, _spell;

    private void Start()
    {
        _bg.clip = FXs.BG;
        _bg.Play();
        _spell.clip = FXs.SpellCastSFX;

        foreach (GameObject go in FindObjectsOfType<GameObject>())
        {
            if (go.CompareTag("Enemy"))
            {
                go.GetComponent<AudioSource>().clip = FXs.EnemyDMGSFX;
            }
            else if (go.CompareTag("Player"))
            {
                go.GetComponent<AudioSource>().clip = FXs.PlayerDMGSFX;
            }
        }
    }

}
