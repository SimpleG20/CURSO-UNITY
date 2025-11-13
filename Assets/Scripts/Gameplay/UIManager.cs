using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// NEW
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text m_ammoTx;
    [SerializeField] private Image m_healthBarFill;

    [SerializeField] private TMP_Text m_enemiesDiedCountTx;

    [SerializeField] private GameObject m_gameOverPanel;
    [SerializeField] private Button m_restartBt;
    [SerializeField] private Button m_backMenuBt;

    public void Initialize()
    {
        m_gameOverPanel.SetActive(false);

        m_restartBt.onClick.AddListener(() =>
        {
            Loader.Load(EScenes.Game);
        });
        m_backMenuBt.onClick.AddListener(() =>
        {
            Loader.Load(EScenes.MainMenu);
        });

        m_enemiesDiedCountTx.text = "00";
        m_healthBarFill.fillAmount = 1f;
        m_ammoTx.text = "";
    }

    public void ShowGameOverPanel()
    {
        m_gameOverPanel.SetActive(true);
    }

    public void UpdateEnemiesDiedCount(int count)
    {
        m_enemiesDiedCountTx.text = count.ToString("00");
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        m_healthBarFill.fillAmount = (float)currentHealth / maxHealth;
    }

    public void UpdateAmmoCount(int currentAmmo, int maxAmmo)
    {
        if (currentAmmo == 0 && maxAmmo == 0)
        {
            m_ammoTx.text = "";
        }
        else
        {
            m_ammoTx.text = $"Ammo: {currentAmmo} / {maxAmmo}";
        }
    }
}
