using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [field: SerializeField] public string Name { get; protected set; }
    public int Health { get; protected set; }
    public bool IsAlive { get; protected set; }

    [SerializeField] protected float m_Speed;
    [SerializeField] protected float m_Strength;
    [SerializeField] protected float m_AttackCooldown;

    protected Vector2 m_MovementDirection;
    protected float m_lastTimeAttacked;

    protected void Awake()
    {
        IsAlive = true;
        Setup();
    }
    protected abstract void Setup();

    protected void Update()
    {
        if (!IsAlive) return;
        UpdateLogic();
    }
    protected abstract void UpdateLogic();


    protected abstract bool CanWalk();
    protected abstract void Walk();

    protected abstract bool CanAttack();
    public abstract void Attack();

    protected abstract bool CanTakeDamage();
    public abstract void TakeDamage();

    protected abstract void Die();
}