using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    public event Action<int> CastSpellHandler;
    public event Action<KeyCode> SpellKeyHandler;
    float _cooldown = Mathf.Infinity;
    bool _isCasting = false;
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        Spell.SpellCastedHandler += OnCastSpell;
    }
    private void OnDisable()
    {
        Spell.SpellCastedHandler -= OnCastSpell;
    }

    private void OnCastSpell()
    {
        _isCasting = false;
    }

    private void Update()
    {
        _cooldown += Time.deltaTime;
        if (_cooldown < 2f || _isCasting)
        {
            return;
        }

        Cast();
    }

    private void Cast()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnCast(0, KeyCode.Mouse0);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnCast(1, KeyCode.Mouse1);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
            OnCast(2, KeyCode.Space);

    }

    private void OnCast(int index, KeyCode key)
    {
        _isCasting = true;
        CastSpellHandler.Invoke(index);
        SpellKeyHandler.Invoke(key);
        _cooldown = 0;
        _animator.SetTrigger("cast");
    }
}
