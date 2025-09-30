using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : BaseCharacter
{
    [SerializeField] private PlayerInput m_playerInput;
    [SerializeField] private Transform m_attackPivot;

    private InputAction m_walkAction;
    private InputAction m_attackAction;

    private bool m_rotatingPivot;
    private float m_lastAttackTime;

    protected override void Setup()
    {
        m_playerInput.SwitchCurrentActionMap("Player");
        m_playerInput.ActivateInput();

        m_walkAction = m_playerInput.actions["Move"];
        m_attackAction = m_playerInput.actions["Attack"];

        m_walkAction.performed += ctx => m_MovementDirection = ctx.ReadValue<Vector2>();
        m_walkAction.canceled += ctx => m_MovementDirection = Vector2.zero;
        m_attackAction.performed += ctx => Attack();
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
        if (!m_IsAlive) return false;
        return true;
    }
    protected override void Walk()
    {
        if (m_MovementDirection == Vector2.zero) return;
        transform.Translate(m_MovementDirection * m_Speed * Time.deltaTime, Space.World);
    }

    protected override bool CanAttack()
    {
        if (m_rotatingPivot) return false;
        if (Time.time - m_lastAttackTime < m_Cooldown) return false;

        return true;
    }
    protected override void Attack()
    {
        if (CanAttack() == false) return;

        m_lastAttackTime = Time.time;
        StartCoroutine(RotatePivot());
    }
    public override void TakeDamage(int damage)
    {
    }

    protected override void Die()
    {
    }

    private IEnumerator RotatePivot()
    {
        bool completedCycle = false;
        m_rotatingPivot = true;
        while (!completedCycle)
        {
            if (m_attackPivot.rotation.eulerAngles.z >= 358)
            {
                m_rotatingPivot = false;
                completedCycle = true;
                m_AttackTriggered = false;
                m_attackPivot.rotation = Quaternion.Euler(0, 0, 0);
                yield break;
            }
            m_attackPivot.Rotate(Vector3.forward * 180 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
