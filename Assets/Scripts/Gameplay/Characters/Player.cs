using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : BaseCharacter
{
    [SerializeField] private PlayerInput m_playerInput;

    private int m_currentWeaponIndex;
    private Vector3 m_scale;

    protected override void Setup()
    {
        Health = MaxHealth;

        GameplayManager.OnPlayerHealthChanged?.Invoke(Health, MaxHealth);

        m_scale = m_Animator.transform.localScale;

        m_currentWeaponIndex = 0;
        UpdateCurrentWeapon();

        m_playerInput.actions["Move"].performed += ctx => HandleOnMovement(ctx.ReadValue<Vector2>());
        m_playerInput.actions["Move"].canceled += ctx => HandleOnMovement(Vector2.zero);

        m_playerInput.actions["Attack"].performed += ctx => Attack();

        m_playerInput.actions["ChangeWeapon"].performed += HandleChangeWeapon;
    }

    // NEW
    private void HandleOnMovement(Vector2 direction)
    {
        m_MovementDirection = direction;
        if (m_MovementDirection == Vector2.zero)
        {
            m_Animator.SetInteger("Speed", 0);
        }
        else
        {
            m_Animator.transform.localScale = new Vector3(Mathf.Sign(m_MovementDirection.x) * m_scale.x , m_scale.y, m_scale.z);
            m_Animator.SetInteger("Speed", 1);
        }
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

        // NEW
        if (m_CurrentWeapon is Gun gun)
        {
            GameplayManager.OnPlayerAmmoChanged?.Invoke(gun.CurrentAmmo, gun.MaxAmmo);
        }
        else
        {
            GameplayManager.OnPlayerAmmoChanged?.Invoke(0, 0);
        }
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

    // NEW
    public override void Attack()
    {
        base.Attack();
        if (m_CurrentWeapon is Gun gun)
        {
            GameplayManager.OnPlayerAmmoChanged?.Invoke(gun.CurrentAmmo, gun.MaxAmmo);
        }
    }
    // NEW
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (Health > 0)
        {
            GameplayManager.OnPlayerHealthChanged?.Invoke(Health, MaxHealth);
        }
    }

    protected override void DieLogic()
    {
        GameplayManager.OnPlayerDied?.Invoke();
    }
}
