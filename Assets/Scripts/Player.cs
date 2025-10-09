using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : BaseCharacter
{
    [SerializeField] private PlayerInput m_playerInput;
    [SerializeField] private Transform m_attackPivot;

    [SerializeField] private float m_pivotRotationSpeed = 180;

    private bool m_swordAnimation;

    protected override void Setup()
    {
        m_playerInput.actions["Move"].performed += ctx => m_MovementDirection = ctx.ReadValue<Vector2>();
        m_playerInput.actions["Move"].canceled += ctx => m_MovementDirection = Vector2.zero;

        m_playerInput.actions["Attack"].performed += ctx => Attack();
    }

    protected override void UpdateLogic()
    {
        if (CanWalk())
        {
            Walk();
        }
    }

    protected override bool CanWalk()
    {
        return m_MovementDirection != Vector2.zero;
    }
    protected override void Walk()
    {
        transform.Translate(m_MovementDirection * m_Speed * Time.deltaTime);
    }


    public override void Attack()
    {
        if (!CanAttack()) return;

        m_lastTimeAttacked = Time.time;

        StartCoroutine(RotateSword());
    }
    protected override bool CanAttack()
    {
        if (m_swordAnimation) return false;
        if (Time.time - m_lastTimeAttacked < m_AttackCooldown) return false;

        return true;
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

    protected override bool CanTakeDamage()
    {
        return true;
    }
    public override void TakeDamage(int damage)
    {
    }

    protected override void Die()
    {
    }
}
