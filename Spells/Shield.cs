using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Spell
{
    [SerializeField] float _duration;
    [SerializeField] GameObject _fx;
    PlayerHealth _health;
    private void Awake()
    {
        _health = GetComponentInParent<PlayerHealth>();
    }
    public override void Cast()
    {
        base.Cast();
        print("Shield");

        StartCoroutine(ShieldRoutine());
    }

    IEnumerator ShieldRoutine()
    {
        _fx.SetActive(true);
        _health.IsInvulnerable = true;
        yield return new WaitForSeconds(_duration);
        _health.IsInvulnerable = false;
        _fx.SetActive(false);

    }

}
