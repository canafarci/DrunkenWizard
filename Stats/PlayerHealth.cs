using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    HealthBar _bar;
    public bool IsInvulnerable = false;
    protected override void Awake()
    {
        base.Awake();
        _bar = FindObjectOfType<HealthBar>();
    }
    public override void TakeDamage(int damage)
    {
        if (IsInvulnerable) { return; }
        base.TakeDamage(damage);
        float pct = (float)_currentHealth / (float)_health;
        _bar.SetHealthBar(pct);
    }
    protected override void Die()
    {
        //base.Die();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetHealth()
    {
        _currentHealth = _health;
        _bar.SetHealthBar(1);
    }
}
