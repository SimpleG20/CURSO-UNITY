using UnityEngine;
using UnityEngine.UI;

public enum EScenes { MainMenu = 0, Game = 1 }

// NEW
public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button m_startBt;
    [SerializeField] private AudioSource m_backgroundMusicSource;
    [SerializeField] private AudioClip m_backgroundMusicClip;

    private void Awake()
    {
        m_startBt.onClick.AddListener(OnStartButtonClicked);

        AudioListener.volume = 1;
    }

    private void Start()
    {
        m_backgroundMusicSource.clip = m_backgroundMusicClip;
        m_backgroundMusicSource.volume = 0.5f;
        m_backgroundMusicSource.Play();
    }

    private void OnStartButtonClicked()
    {
        Loader.Load(EScenes.Game);
    }
}
