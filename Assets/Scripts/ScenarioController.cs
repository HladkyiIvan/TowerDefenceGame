using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScenarioController : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public GameObject LivesPanel;

    public TextMeshProUGUI EndGameText;
    public GameObject EndGamePanel;

    public ScenarioData ScenarioData;

    private int EnemiesToKill;
    private int KillCounter = 0;
    public static int Money = 10;
    private int HP = 5;
    private List<GameObject> MarkersListHP = new List<GameObject>();

    private void Awake()
    {
        Events.OnEnemyKilled += AddRemoveMoney;
        Events.OnTowerPurchased += AddRemoveMoney;
        Events.OnBaseDamageReceived += AddRemoveHP;
    }
    private void OnDestroy()
    {
        Events.OnEnemyKilled -= AddRemoveMoney;
        Events.OnTowerPurchased -= AddRemoveMoney;
        Events.OnBaseDamageReceived -= AddRemoveHP;
    }
    // Start is called before the first frame update
    void Start()
    {
        Events.InitializeWaves(ScenarioData.Waves);

        EnemiesToKill = ScenarioData.Waves.Sum(item => item.EnemiesInWave * ScenarioData.NumberOfSpawns);
        EndGamePanel.SetActive(false);

        Money = ScenarioData.StartingMoney;
        MoneyText.text = Money.ToString();

        foreach (Transform child in LivesPanel.transform)
        {
            child.gameObject.SetActive(true);
            MarkersListHP.Add(child.gameObject);
        }
    }

    void AddRemoveMoney(int change)
    {
        if (change > 0)
        {
            UpdateKillCounter();
        }
        
        Money += change;
        MoneyText.text = Money.ToString();
    }

    void AddRemoveHP(int change)
    {
        bool subtration = change < 0;

        for (int i = 0; i < Mathf.Abs(change); i++)
        {
            int index;
            
            if (subtration)
            {
                index = HP - i - 1;
            }
            else
            {
                index = HP + i;
            }

            if (index > 4)
            {
                HP = 5;
                return;
            }

            if (index <= 0)
            {
                HP = 0;
                ShowLoseMessage();
                return;
                //SceneManager.LoadScene("SampleScene");
            }

            MarkersListHP[index].SetActive(!subtration);
        }

        HP += change;

        return;
    }

    public void UpdateKillCounter()
    {
        KillCounter++;
        
        if (KillCounter == EnemiesToKill)
        {
            ShowWinMessage();
        }
    }

    public void ShowLoseMessage()
    {
        EndGameText.text = "Your base was destroyed! Retreat!";
        EndGamePanel.SetActive(true);
    }

    public void ShowWinMessage()
    {
        EndGameText.text = "Great job! The base is safe now!";
        EndGamePanel.SetActive(true);
    }

    public void RestartPressed()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void MenuPressed()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
