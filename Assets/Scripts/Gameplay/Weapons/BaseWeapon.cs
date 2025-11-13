using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected int m_Damage = 10;
    [SerializeField] protected float m_AttackCooldown = 1f;

    [SerializeField] protected GameObject m_Root;
    [SerializeField] protected SpriteRenderer m_SpriteRenderer;


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
        m_Root.SetActive(value);
    }

    public virtual bool CanUse()
    {
        if (m_Owner == null) return false;
        if (!m_Active) return false;
        if (Time.time - m_LastAttackTime < m_AttackCooldown) return false;
        return true;
    }
    public abstract void Use();
}
