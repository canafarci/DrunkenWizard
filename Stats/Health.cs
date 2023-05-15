using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HealthPoints { get { return _health; } }
    [SerializeField] protected int _health;
    protected int _currentHealth;
    LevelChanger _levelChanger;
    AudioSource _source;
    GameObject _enemyDeath, _playerDMG, _enemyDMG;
    EnemyHealthBar _healthBar;

    protected virtual void Awake()
    {
        _currentHealth = _health;
        _levelChanger = FindObjectOfType<LevelChanger>();
        _source = GetComponent<AudioSource>();
        _healthBar = GetComponent<EnemyHealthBar>();
    }

    private void Start()
    {
        FXSetter setter = FindObjectOfType<FXSetter>();
        _enemyDeath = setter.FXs.EnemyDeath;
        _playerDMG = setter.FXs.PlayerDMG;
        _enemyDMG = setter.FXs.EnemyDMG;
    }
    private void OnEnable()
    {
        if (gameObject.CompareTag("Enemy"))
            _levelChanger.EnemiesRemaining++;
    }

    public virtual void TakeDamage(int damage)
    {
        _source.Play();

        _currentHealth -= damage;

        GameObject fx;

        if (gameObject.CompareTag("Enemy"))
        {
            fx = Instantiate(_enemyDMG, transform.position, _enemyDMG.transform.rotation);
            _healthBar.OnTakeDamage();
        }
        else
            fx = Instantiate(_playerDMG, transform.position, _playerDMG.transform.rotation);
        fx.transform.parent = transform;

        Destroy(fx, 2f);

        if (_currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        _levelChanger.OnEnemyDead();

        if (gameObject.CompareTag("Enemy"))
        {
            GameObject fx;
            fx = Instantiate(_enemyDeath, transform.position, _enemyDeath.transform.rotation);
            Destroy(fx, 2f);
        }

        Destroy(gameObject, 0.05f);
    }
}
