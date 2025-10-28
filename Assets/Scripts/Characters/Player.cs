using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : BaseCharacter
{
    [SerializeField] private PlayerInput m_playerInput;

    private int m_currentWeaponIndex;

    protected override void Setup()
    {
        m_currentWeaponIndex = 0;
        UpdateCurrentWeapon();

        m_playerInput.actions["Move"].performed += ctx => m_MovementDirection = ctx.ReadValue<Vector2>();
        m_playerInput.actions["Move"].canceled += ctx => m_MovementDirection = Vector2.zero;

        m_playerInput.actions["Attack"].performed += ctx => Attack();

        m_playerInput.actions["ChangeWeapon"].performed += HandleChangeWeapon;
    }
    private void HandleChangeWeapon(InputAction.CallbackContext obj)
    {
        m_currentWeaponIndex = (m_currentWeaponIndex + 1) % m_Weapons.Length;
        UpdateCurrentWeapon();
    }

    protected override bool CanWalk()
    {
        return m_MovementDirection != Vector2.zero;
    }
    protected override bool CanAttack()
    {
        if (!m_CurrentWeapon.CanUse()) return false;

        return true;
    }

    private void UpdateCurrentWeapon()
    {
        m_CurrentWeapon?.SetActive(false);
        m_CurrentWeapon = m_Weapons[m_currentWeaponIndex];
        m_CurrentWeapon.SetActive(true);
    }
    protected override void UpdateLogic()
    {
        if (CanWalk())
        {
            Walk();
        }
    }

    protected override void Walk()
    {
        transform.Translate(m_MovementDirection * m_Speed * Time.deltaTime);
    }
    protected override bool CanTakeDamage()
    {
        return true;
    }
    protected override void DieLogic()
    {
        //GameplayManager.OnPlayerDied?.Invoke();
    }
}
