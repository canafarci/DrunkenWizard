using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Transform _parent;
    [SerializeField] GameObject _prefab;
    Health _health;
    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    

    private void Start()
    {
        int hp = _health.HealthPoints;
        if (hp == 1) { return; }

        _parent.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(hp / 2f, 0.5f);

        for (int i = 0; i < hp; i++)
        {
            GameObject fx = GameObject.Instantiate(_prefab, _parent.transform.position, Quaternion.identity, _parent);
            fx.transform.localPosition = Vector3.zero;
        }

    }

    public void OnTakeDamage()
    {
        int hp = _health.HealthPoints;
        if (hp == 1) { return; }

        Destroy(_parent.GetChild(0).gameObject);

    }
}
