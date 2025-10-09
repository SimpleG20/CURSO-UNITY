using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    public string Name { get; protected set; }
    public int Health { get; protected set; }

    [SerializeField] protected float m_Speed;
    [SerializeField] protected float m_Strength;
    [SerializeField] protected bool m_IsAlive;

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