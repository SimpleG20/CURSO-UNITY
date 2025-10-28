using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class GameplayManager : MonoBehaviour
{
    public static Action OnEnemyDied { get; set; }
    public static Action OnPlayerDied { get; set; }

    [SerializeField] private float m_maxGameplayTimeSec = 30;
    [SerializeField] private float m_enemiesSpawnTimerSec = 9;
    [SerializeField] private bool m_canInstantiateEnemies;
    [SerializeField] private Transform m_enemiesParent;

    [SerializeField] private Player m_player;
    [SerializeField] private BaseEnemy[] m_baseEnemies;
    [SerializeField] private Transform[] m_enemySpawnPoints;

    private int m_enemiesDied;
    private float m_gameplayTimer;
    private bool m_isGameRunning;

    private void Start()
    {
        OnEnemyDied += HandleOnEnemyDied;
        OnPlayerDied += HandleOnPlayerDied;

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

        print("Ended Gameplay");

        var enemies = FindObjectsByType<BaseEnemy>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }

        var player = FindFirstObjectByType<Player>();
        if (player != null) Destroy(player.gameObject);
    }
    private void HandleOnEnemyDied()
    {
        m_enemiesDied++;

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
