using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHandler : MonoBehaviour
{
    [SerializeField] Spell[] _spells;
    [SerializeField] AudioSource _source;
    public void OnCastSpell(int index)
    {
        _source.Play();
        _spells[index].Cast();
    }
}