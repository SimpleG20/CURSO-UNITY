using UnityEngine;

public abstract class BaseEnemy : BaseCharacter
{
    protected Player m_Player;

    protected override void Setup()
    {
        m_Player = FindFirstObjectByType<Player>();
    }

    protected override void DieLogic()
    {
        // NEW
        var rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        GameplayManager.OnEnemyDied?.Invoke();
    }
}
