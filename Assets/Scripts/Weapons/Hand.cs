using UnityEngine;

public class Hand : BaseWeapon
{
    public override void Use()
    {
        if (Random.Range(0, 2) == 0)
        {
            m_LastAttackTime = Time.time;
            FindFirstObjectByType<Player>().TakeDamage(m_Damage);
        }
    }
}