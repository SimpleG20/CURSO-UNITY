using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : BaseCharacter
{
    [SerializeField] private PlayerInput m_playerInput;

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

    protected override bool CanAttack()
    {
        if (!m_CurrentWeapon.CanUse()) return false;

        return true;
    }
    protected override bool CanTakeDamage()
    {
        return true;
    }

    protected override void DieLogic()
    {
    }
}
