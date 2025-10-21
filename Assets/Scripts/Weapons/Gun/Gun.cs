using UnityEngine;

public class Gun : BaseWeapon
{
    [SerializeField] private Bullet m_bulletPrefab;
    [SerializeField] private Transform m_firePoint;
    [SerializeField] private int m_ammoCapacity = 12;

    private int m_currentAmmo;
    private Quaternion m_gunDirection;

    private void Start()
    {
        m_currentAmmo = m_ammoCapacity;
    }

    private void Update()
    {
        if (!m_Active) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - m_firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        m_gunDirection = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = m_gunDirection;
    }

    public override bool CanUse()
    {
        if (m_Owner == null) return false;
        if (!m_Active) return false;
        if (m_currentAmmo <= 0) return false;
        if (Time.time - m_LastAttackTime < m_AttackCooldown) return false;

        return true;
    }
    public override void Use()
    {
        m_currentAmmo--;
        m_LastAttackTime = Time.time;
        var bullet = Instantiate(m_bulletPrefab, m_firePoint.position, m_gunDirection);
        bullet.Setup(m_Damage);
    }

    public void Reload()
    {
        m_currentAmmo = m_ammoCapacity;
    }
}
