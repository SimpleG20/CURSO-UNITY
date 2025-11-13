using System.Collections;
using UnityEngine;

public class Sword : BaseWeapon
{
    [SerializeField] private Transform m_attackPivot;
    [SerializeField] private float m_pivotRotationSpeed = 360;

    private bool m_swordAnimation;
    private Quaternion m_weaponDirection;

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

    // NEW
    private void Update()
    {
        if (!m_Active) return;
        if (m_swordAnimation) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - m_Owner.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        m_weaponDirection = Quaternion.Euler(new Vector3(0, 0, angle));
        m_attackPivot.rotation = m_weaponDirection;

        if (m_attackPivot.eulerAngles.z > 90 && m_attackPivot.eulerAngles.z < 270)
        {
            m_SpriteRenderer.flipY = true;
        }
        else
        {
            m_SpriteRenderer.flipY = false;
        }
    }

    private IEnumerator RotateSword()
    {
        if (m_swordAnimation) yield break;

        m_swordAnimation = true;
        bool completedCycle = false;

        // NEW

        // Independent to current rotation of m_attackPivot.eulerAngles.z it will rotate 360 degrees
        float rotatedAngle = 0f;
        while (!completedCycle)
        {
            float rotationThisFrame = m_pivotRotationSpeed * Time.deltaTime;
            m_attackPivot.eulerAngles += Vector3.forward * rotationThisFrame;
            rotatedAngle += rotationThisFrame;
            if (rotatedAngle >= 360f)
            {
                m_attackPivot.rotation = m_weaponDirection;
                m_swordAnimation = false;
                completedCycle = true;
                yield break;
            }
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