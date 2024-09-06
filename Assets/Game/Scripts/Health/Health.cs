using System;
using UnityEngine;

public class Health
{
    public int _maxHealth { get; private set; }
    private int _health;
    public int HP 
    {
        get
        {
            return _health;
        }
        private set 
        {
            _health = Mathf.Clamp(value,0, _maxHealth);
            OnHealthChanged?.Invoke(_health);
        } 
    }

    public event Action<int> OnHealthChanged;

    public Health(int maxHealth)
    {
        _maxHealth = maxHealth;
        HP = maxHealth;
    }
    public void GetDamage(int damage)
    {
        HP -= damage;
    }
    public void Heal(int health)
    {
        HP += health;
    }
}
