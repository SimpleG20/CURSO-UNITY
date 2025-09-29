using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    public string Name { get; protected set; }
    public int Health { get; protected set; }

    [SerializeField] protected float m_speed;
    [SerializeField] protected float m_strength;
    [SerializeField] protected bool m_isAlive;

    protected void Awake()
    {
        Setup();
    }
    protected abstract void Setup();


    protected abstract void Walk();
    public abstract void Attack();
    public abstract void TakeDamage();
    protected abstract void Die();
}