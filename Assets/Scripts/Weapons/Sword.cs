using System.Collections;
using UnityEngine;

public class Sword : BaseWeapon
{
    [SerializeField] private Transform m_attackPivot;
    [SerializeField] private float m_pivotRotationSpeed = 360;

    private bool m_swordAnimation;

    public override bool CanUse()
    {
        if (!base.CanUse()) return false;
        return !m_swordAnimation;
    }
    public override void Use()
    {
        if (!CanUse()) return;

        m_LastAttackTime = Time.time;

        StartCoroutine(RotateSword());
    }

    private IEnumerator RotateSword()
    {
        bool completedCycle = false;
        m_swordAnimation = true;
        while (!completedCycle)
        {
            if (m_attackPivot.rotation.eulerAngles.z >= 358)
            {
                m_swordAnimation = false;
                completedCycle = true;
                m_attackPivot.rotation = Quaternion.Euler(0, 0, 0);
                yield break;
            }
            m_attackPivot.Rotate(Vector3.forward * m_pivotRotationSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseCharacter character) && character != m_Owner
            && m_swordAnimation)
        {
            character.TakeDamage(m_Damage);
        }
    }
}
