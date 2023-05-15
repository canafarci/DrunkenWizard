using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroller : MonoBehaviour
{
    [SerializeField] Transform _pointsHolder;
    Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    [SerializeField] float _attackDistance, _attackRate;
    float _cooldown = Mathf.Infinity;
    Transform _player;
    PlayerHealth _playerHealth;
    bool _isAttacking;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Start()
    {
        _pointsHolder.parent = null;

        int count = _pointsHolder.childCount;

        points = new Transform[count];
        print(count);

        for (int i = 0; i < count; i++)
        {
            points[i] =
            _pointsHolder.GetChild(i);
            print(points[i]);
            print(_pointsHolder.GetChild(i));
        }

        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GotoNextPoint();
        StartCoroutine(CheckForAttack());
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    IEnumerator CheckForAttack()
    {
        while (true)
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);

            if (distance < _attackDistance)
            {
                _isAttacking = true;
            }
            else
            {
                _isAttacking = false;
            }


            if (_isAttacking && _cooldown >= _attackRate)
            {
                Attack();
                _cooldown = 0;
            }

            yield return new WaitForSeconds(_attackRate);
            _cooldown += _attackRate;
        }
    }

    void Attack()
    {
        _playerHealth.TakeDamage(1);
    }

}

