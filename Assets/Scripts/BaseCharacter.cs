using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    public string Name { get; protected set; }
    public int Health { get; protected set; }

    [SerializeField] protected float m_Speed;
    [SerializeField] protected float m_Strength;
    [SerializeField] protected float m_Cooldown;
    
    protected bool m_IsAlive;
    protected Vector2 m_MovementDirection;
    protected bool m_AttackTriggered;

    protected void Awake()
    {
        m_IsAlive = true;
        Setup();
    }
    protected abstract void Setup();

    protected void Update()
    {
        if (!m_IsAlive) return;
        UpdateLogic();
    }
    protected abstract void UpdateLogic();

    protected abstract void Walk();
    protected abstract bool CanWalk();

    protected abstract bool CanAttack();
    protected abstract void Attack();
    public abstract void TakeDamage(int damage);
    protected abstract void Die();
}