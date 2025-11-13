using System.Collections;
using UnityEngine;

public class Gun : BaseWeapon
{
    [SerializeField] private Bullet m_bulletPrefab;
    [SerializeField] private int m_ammoCapacity = 12;
    [SerializeField] private int m_bulletsPerShot = 1;

    [SerializeField] private Transform m_firePoint;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    // NEW
    public int MaxAmmo => m_ammoCapacity;
    public int CurrentAmmo => m_currentAmmo;

    private int m_currentAmmo;
    private Quaternion m_gunDirection;


    private void Awake()
    {
        m_currentAmmo = m_ammoCapacity;
    }

    private void Update()
    {
        if (!m_Active) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - m_Owner.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        m_gunDirection = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = m_gunDirection;

        // NEW
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            m_SpriteRenderer.flipY = true;
        }
        else
        {
            m_SpriteRenderer.flipY = false;
        }
    }

    public override bool CanUse()
    {
        if (!base.CanUse()) return false;
        if (m_bulletPrefab == null) return false;
        if (m_currentAmmo <= 0) return false;

        return true;
    }
    public override void Use()
    {
        m_LastAttackTime = Time.time;
        StartCoroutine(InstantiateBullet());
    }
    private IEnumerator InstantiateBullet()
    {
        for (int i = 0; i < m_bulletsPerShot; i++)
        {
            m_currentAmmo--;
            var bullet = Instantiate(m_bulletPrefab, m_firePoint.position, m_gunDirection);
            bullet.Setup(m_Damage);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Reload()
    {
        m_currentAmmo = m_ammoCapacity;
    }
}
