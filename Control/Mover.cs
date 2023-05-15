using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float _speed, _rotateSpeed;
    InputReader _reader;
    Rigidbody _rigidbody;
    Animator _animator;
    public bool IsTeleporting = false;
    private void Awake()
    {
        _reader = GetComponent<InputReader>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (_reader.Direction == Vector3.zero || IsTeleporting)
        {
            _animator.SetBool("running", false);
            return;
        }
        else
        {
            _animator.SetBool("running", true);
        }

        Vector3 pos = transform.position;
        pos += _reader.Direction * Time.deltaTime * _speed;

        var step = _speed * Time.deltaTime;

        Vector3 dir = pos - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * _rotateSpeed);

        _rigidbody.MovePosition(pos);

    }

}
