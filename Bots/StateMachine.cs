using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] float _attackDistance, _attackRate;
    [SerializeField] bool _isRanged = false;
    [SerializeField] GameObject _projectile;
    Transform _player;
    NavMeshMover _mover;
    PlayerHealth _playerHealth;
    bool _isAttacking;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _mover = GetComponent<NavMeshMover>();
    }

    private void Start()
    {
        StartCoroutine(CheckForAttack());
    }
    private void Update()
    {
        if (!_isRanged) { return; }
        transform.LookAt(_player.position);
    }
    IEnumerator CheckForAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackRate / 2f);
            float distance = Vector3.Distance(_player.transform.position, transform.position);

            if (distance < _attackDistance)
            {
                _isAttacking = true;
                _mover.Stop();
            }
            else
            {
                _isAttacking = false;
                _mover.Move(_player.position);
            }

            yield return new WaitForSeconds(_attackRate / 2f);

            if (_isAttacking)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        if (!_isRanged)
            _playerHealth.TakeDamage(1);
        else
        {
            GameObject proj = GameObject.Instantiate(_projectile, transform.position + transform.forward + Vector3.up * 3f, Quaternion.identity);
            proj.transform.LookAt(_player.transform);
            Destroy(proj, 8f);
        }
    }
}
