using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RollDice : MonoBehaviour
{
    [SerializeField] Vector3 _randomness;
    [SerializeField] float _rollTime;
    [SerializeField] Vector3[] _possibleRolls;
    Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        _rigidbody.AddTorque(new Vector3(Random.Range(-_randomness.x, _randomness.x),
                   Random.Range(-_randomness.y, _randomness.y),
                   Random.Range(-_randomness.z, _randomness.z)),
                   ForceMode.Impulse);

        yield return new WaitForSeconds(3 * _rollTime / 4f);
        _rigidbody.angularVelocity = Vector3.zero;

        // Vector3 target = _possibleRolls.ToList().OrderBy(x => Vector3.SignedAngle(transform.rotation.eulerAngles, x, Vector3.up)).Last();
        float time = _rollTime / 4f;
        // yield return new WaitForEndOfFrame();
        Vector3 target = _possibleRolls[(int)Random.Range(0, _possibleRolls.Length)];

        while (time > 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(target), Time.deltaTime * 1000);
            yield return new WaitForEndOfFrame();
            time -= Time.deltaTime;
        }
    }
}
