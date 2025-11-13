using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private float m_speed = 10;

    private int m_damage;

    private void Start()
    {
        m_rb.linearVelocity = transform.right * m_speed;
        Destroy(gameObject, 5f);
    }
    public void Setup(int damage)
    {
        m_damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out BaseCharacter character))
        {
            character.TakeDamage(m_damage);
        }
        Destroy(gameObject);
    }
}
