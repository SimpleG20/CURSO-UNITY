using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [field: SerializeField] public string Name { get; protected set; }
    public int Health { get; protected set; }
    public bool IsAlive { get; protected set; }

    [SerializeField] protected float m_Speed;

    [SerializeField] protected BaseWeapon[] m_Weapons;
    [SerializeField] protected BaseWeapon m_CurrentWeapon;

    protected Vector2 m_MovementDirection;

    protected void Awake()
    {
        IsAlive = true;

        for (int i = 0; i < m_Weapons.Length; i++)
        {
            m_Weapons[i].SetOwner(this);
        }

        m_CurrentWeapon = m_Weapons[0];

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
    public void Attack()
    {
        if (m_CurrentWeapon == null) return;
        if (!CanAttack()) return;

        m_CurrentWeapon.Use();
    }

    protected abstract bool CanTakeDamage();
    public void TakeDamage(int damage)
    {
        if (!CanTakeDamage()) return;

        Health -= damage;
        print($"{Name} took {damage} damage, remaining health: {Health}");
        if (Health <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}