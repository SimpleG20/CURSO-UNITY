using UnityEngine;
using UnityEngine.InputSystem;

public class Player : BaseCharacter
{
    [SerializeField] private PlayerInput m_playerInput;

    private Vector2 m_movementDiretion;
    protected override void Setup()
    {
        m_playerInput.actions["Move"].performed += ctx => m_movementDiretion = ctx.ReadValue<Vector2>();
        m_playerInput.actions["Move"].canceled += ctx => m_movementDiretion = Vector2.zero;
    }
    private void Update() => transform.Translate(m_movementDiretion * m_Speed * Time.deltaTime);
    protected override void Walk()
    {
    }

    public override void Attack()
    {
    }
    public override void TakeDamage()
    {
    }

    protected override void Die()
    {
    }
}
