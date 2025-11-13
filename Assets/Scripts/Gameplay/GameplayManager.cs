using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class GameplayManager : MonoBehaviour
{
    public static Action OnEnemyDied { get; set; }
    public static Action OnPlayerDied { get; set; }

    // NEW
    public static Action<int, int> OnPlayerHealthChanged { get; set; }
    public static Action<int, int> OnPlayerAmmoChanged { get; set; }


    [SerializeField] private float m_maxGameplayTimeSec = 30;
    [SerializeField] private float m_enemiesSpawnTimerSec = 9;
    [SerializeField] private bool m_canInstantiateEnemies;
    [SerializeField] private Transform m_enemiesParent;

    [SerializeField] private Player m_player;
    [SerializeField] private BaseEnemy[] m_baseEnemies;
    [SerializeField] private Transform[] m_enemySpawnPoints;

    // NEW
    [SerializeField] private UIManager m_uiManager;

    private int m_enemiesDied;
    private float m_gameplayTimer;
    private bool m_isGameRunning;

    private void Awake()
    {
        OnEnemyDied += HandleOnEnemyDied;
        OnPlayerDied += HandleOnPlayerDied;
        OnPlayerAmmoChanged += m_uiManager.UpdateAmmoCount;
        OnPlayerHealthChanged += m_uiManager.UpdateHealthBar;

        m_uiManager.Initialize();
    }
    private void Start()
    {
        m_isGameRunning = true;
        m_player.Initialize();

        StartCoroutine(EnemySpawnRoutine());

        m_gameplayTimer = Time.time;
    }
    private void Update()
    {
        if (!m_isGameRunning) return;

        if (Time.time -  m_gameplayTimer > m_maxGameplayTimeSec)
        {
            m_gameplayTimer = Time.time;
            EndGameplay();
        }
    }
    private void HandleOnPlayerDied()
    {
        EndGameplay();
    }
    private void EndGameplay()
    {
        if (!m_isGameRunning) return;
        m_isGameRunning = false;

        var enemies = FindObjectsByType<BaseEnemy>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].TakeDamage(1000);
        }

        var player = FindFirstObjectByType<Player>();
        player.TakeDamage(100);

        // NEW
        m_uiManager.ShowGameOverPanel();
    }
    private void HandleOnEnemyDied()
    {
        m_enemiesDied++;
        // NEW
        m_uiManager.UpdateEnemiesDiedCount(m_enemiesDied);

        if (m_enemiesDied % 2 == 0 && Random.Range(0, 3) == 0)
        {
            InstantiateEnemy();
        }
    }

    private IEnumerator EnemySpawnRoutine()
    {
        if (!m_isGameRunning) yield break;
        if (!m_canInstantiateEnemies) yield break;

        InstantiateEnemy();
        yield return new WaitForSeconds(m_enemiesSpawnTimerSec);

        StartCoroutine(EnemySpawnRoutine());
    }
    private void InstantiateEnemy()
    {
        var position = m_enemySpawnPoints[Random.Range(0, m_enemySpawnPoints.Length)].position;
        int randomIndex = Random.Range(0, m_baseEnemies.Length);
        BaseEnemy enemyInstance = Instantiate(
            m_baseEnemies[randomIndex], 
            position, 
            Quaternion.identity,
            m_enemiesParent
        );
        enemyInstance.Initialize();
    }
}
