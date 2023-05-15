using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fireball : Spell
{
    public KeyCode Key;
    [SerializeField] ParticleSystem _particle;
    [SerializeField] LayerMask _mask;
    [SerializeField] float _speed;
    delegate void onCast();
    onCast _onCast;
    bool _isLookingAtTarget = false;
    Vector3 _target;
    private void OnEnable()
    {
        FindObjectOfType<CastSpell>().SpellKeyHandler += OnStartCastSpell;
    }
    private void OnDisable()
    {
        CastSpell cast = FindObjectOfType<CastSpell>();
        if (cast != null)
            cast.SpellKeyHandler -= OnStartCastSpell;
    }

    private void OnStartCastSpell(KeyCode key)
    {
        Key = key;
    }
    private void Update()
    {
        if (_isLookingAtTarget)
            transform.parent.LookAt(_target);
    }

    public override void Cast()
    {
        _onCast = base.Cast;
        print("Fireball");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _mask))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = transform.parent.position.y;
            _target = hitPoint;
            transform.parent.LookAt(_target);
            StartCoroutine(Release());
            StartCoroutine(LockDirection());
        }
        else
        {
            StartCoroutine(CallOnCast());
        }

    }

    IEnumerator Release()
    {
        GameObject particle = Instantiate(_particle.gameObject, transform.position + transform.parent.forward * 3f + Vector3.up * 2f, transform.parent.rotation);
        Vector3 dir = transform.parent.rotation.eulerAngles;
        dir.z = 0;
        dir.x = 0;
        print(dir);

        particle.SetActive(true);
        particle.GetComponent<CreateProjectile>().Create(dir, _speed);
        Destroy(particle, 1f);
        yield return StartCoroutine(CallOnCast());

    }

    IEnumerator CallOnCast()
    {
        yield return new WaitForEndOfFrame();
        _onCast();
    }

    IEnumerator LockDirection()
    {
        _isLookingAtTarget = true;
        yield return new WaitForSeconds(0.25f);
        _isLookingAtTarget = false;
    }
}
