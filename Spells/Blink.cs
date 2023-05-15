using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : Spell
{
    public KeyCode Key;
    [SerializeField] ParticleSystem _particle;
    [SerializeField] LayerMask _mask;
    Mover _mover;
    Rigidbody _rb;
    delegate void onCast();
    onCast _onCast;
    bool _isLookingAtTarget = false;
    Vector3 _target;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _mover = FindObjectOfType<Mover>();
    }
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

        GameObject particle = Instantiate(_particle.gameObject, transform.position + transform.parent.forward * 3f + Vector3.up * 2f, transform.parent.rotation);
        particle.SetActive(true);
        Destroy(particle, 1f);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _mask))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = transform.parent.position.y;
            _target = hitPoint;
            transform.parent.LookAt(hitPoint);
            StartCoroutine(LockDirection());
            StartCoroutine(Release(hit));
        }
        else
        {
            StartCoroutine(CallOnCast());
        }
    }

    IEnumerator Release(RaycastHit hit)
    {
        _mover.IsTeleporting = true;
        Vector3 hitPoint = hit.point;
        hitPoint.y = transform.parent.position.y;
        _rb.MovePosition(hitPoint);
        yield return new WaitForSeconds(.05f);
        _mover.IsTeleporting = false;


        yield return new WaitForEndOfFrame();
        StartCoroutine(CallOnCast());
        yield return new WaitForSeconds(0.1f);
        GameObject particle = Instantiate(_particle.gameObject, transform.position + transform.parent.forward * 3f + Vector3.up * 2f, transform.parent.rotation);
        particle.SetActive(true);
        Destroy(particle, 1f);
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
