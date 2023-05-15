using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : MonoBehaviour
{
    NavMeshAgent _agent;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    public void Move(Vector3 target)
    {
        _agent.isStopped = false;
        _agent.destination = target;
    }
}
