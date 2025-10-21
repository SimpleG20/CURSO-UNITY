using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected int m_Damage = 10;
    [SerializeField] protected float m_AttackCooldown = 1f;

    protected float m_LastAttackTime = -Mathf.Infinity;
    protected BaseCharacter m_Owner;
    protected bool m_Active = false;

    public void SetOwner(BaseCharacter owner)
    {
        m_Owner = owner;
    }
    public void SetActive(bool value)
    {
        m_Active = value;
    }

    public abstract bool CanUse();
    public abstract void Use();
}
