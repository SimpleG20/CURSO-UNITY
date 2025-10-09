using UnityEngine;

public class Chaser : BaseEnemy
{
    [SerializeField] private float m_distanceToStartChasing = 5f;

    private Vector2 m_distanceFromPlayer;

    protected override void UpdateLogic()
    {
        m_distanceFromPlayer = m_Player.transform.position - transform.position;

        if (CanWalk())
        {
            m_MovementDirection = m_distanceFromPlayer.normalized;
            Walk();
        }
    }
    protected override bool CanWalk()
    {
        if (m_Player == null) return false;
        if (m_Player.IsAlive == false) return false;
        if (m_distanceFromPlayer.magnitude < m_distanceToStartChasing) return false;

        return true;
    }
    protected override void Walk()
    {
        transform.Translate(m_MovementDirection * m_Speed * Time.deltaTime);
    }
    protected override bool CanAttack()
    {
        return true;
    }
    public override void Attack()
    {
        
    }

    public override void TakeDamage()
    {
    }
    protected override bool CanTakeDamage()
    {
        return true;
    }

    protected override void Die()
    {
    }
}
