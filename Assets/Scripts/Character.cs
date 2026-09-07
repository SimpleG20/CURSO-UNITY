using UnityEngine;

public class Character
{
    private int _health;
    private float _speed;
    private Weapon _weapon;

    public bool IsAlive => _health > 0;
    
    public void TakeDamage(int damage, int health)
    {
        _health = health;
        if (!IsAlive)
        {
            Debug.Log("Character is already dead.");
            return;
        }

        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Character has died.");
    }
}

public class Weapon
{
    private string name;
    private int damage;
    private float cooldown;
    private float range;


    
}
