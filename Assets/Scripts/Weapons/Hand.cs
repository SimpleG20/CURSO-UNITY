using UnityEngine;

public class Hand : BaseWeapon
{
    public override void Use()
    {
        if (Random.Range(0, 2) == 0)
        {
            FindFirstObjectByType<Player>().TakeDamage(m_Damage);
        }
    }
}