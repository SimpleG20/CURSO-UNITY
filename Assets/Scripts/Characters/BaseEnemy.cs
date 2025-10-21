using UnityEngine;

public abstract class BaseEnemy : BaseCharacter
{
    protected Player m_Player;

    protected override void Setup()
    {
        m_Player = FindFirstObjectByType<Player>();
    }
}
