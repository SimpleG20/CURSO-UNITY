using UnityEngine;

public class Chaser : BaseEnemy
{
    [SerializeField] private float m_distanceToStartChasing = 5f;

    private Vector2 m_distanceFromPlayer;
    private Vector3 m_scale;
    protected override void Setup()
    {
        base.Setup();
        Health = 75;

        m_CurrentWeapon = m_Weapons[0];
        m_CurrentWeapon.SetOwner(this);
        m_CurrentWeapon.SetActive(true);

        m_scale = m_Animator.transform.localScale;
    }

    protected override void UpdateLogic()
    {
        if (m_Player == null) return;
        m_distanceFromPlayer = m_Player.transform.position - transform.position;

        if (CanWalk())
        {
            m_MovementDirection = m_distanceFromPlayer.normalized;
            Walk();
        }

        if (m_distanceFromPlayer.magnitude < m_distanceToStartChasing)
        {
            print("Ataque");
            Attack();
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
        m_Animator.SetInteger("Speed", m_MovementDirection.sqrMagnitude == 0 ? 0 : 1);

        m_Animator.transform.localScale = new Vector3(Mathf.Sign(m_MovementDirection.x) * m_scale.x, m_scale.y, m_scale.z);

        transform.Translate(m_MovementDirection * m_Speed * Time.deltaTime);
    }
}
