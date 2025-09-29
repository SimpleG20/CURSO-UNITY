using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    private string m_name;
    public int m_health;
    private float m_speed;
    private float m_strength;
    private bool m_isAlive;

    internal bool test;

    private void Walk()
    {

    }

    private void Attack()
    {

    }

    private void TakeDamage()
    {

    }
}

public class Test
{
    private BaseEnemy m_enemy;
    private BaseCharacter m_ally;
    public void Teste()
    {
    }
}
