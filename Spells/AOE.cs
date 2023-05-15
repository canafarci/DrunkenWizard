using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AOE : Spell
{
    [SerializeField] ParticleSystem _particle;
    [SerializeField] LayerMask _mask;
    [SerializeField] float _radius;
    public override void Cast()
    {
        base.Cast();

        print("AOE");

        GameObject particle = Instantiate(_particle.gameObject, transform.position, _particle.transform.rotation);
        particle.SetActive(true);
        Destroy(particle, 4.5f);

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _radius, Vector3.up, _radius, _mask);

        hits.ToList().ForEach(x => x.transform.GetComponent<Health>().TakeDamage(1));

    }
}
