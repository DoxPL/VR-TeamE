using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Game")]
    public Player player;
    public GameObject enemyContainer;
    public bool isMultiplayerMode;

    [Header("UserInterface")]
    public Text healthCounter;
    public Text ammoCounter;
    public Text enemyCounter;
    public Text infoText;

    private bool gameOver = false;
    private float resetTimer = 5f;
    private static Stack<string> levels = new Stack<string>();

    void Start()
    {
        infoText.gameObject.SetActive(false);

        if (levels.Count == 0)
        {
            levels.Push("GameMenu");
            levels.Push("Level2");
        }
    }

    void Update()
    {
        healthCounter.text = "HP: " + player.Health;
        ammoCounter.text = "Amunicja: " + player.Ammunation;

        int aliveEnemyCount = 0;

        foreach(Enemy enemy in enemyContainer.GetComponentsInChildren<Enemy>())
        {
            if (!enemy.IsKilled)
            {
                aliveEnemyCount++;
            }
        }

        enemyCounter.text = "Przeciwnicy: " + aliveEnemyCount;

        if(aliveEnemyCount == 0)
        {
            gameOver = true;
            infoText.gameObject.SetActive(true);
            infoText.text = "Poziom ukończony!";
        }

        if(player.IsKilled)
        {
            gameOver = true;
            infoText.gameObject.SetActive(true);
            infoText.text = "Przegrana";
        }

        if(gameOver)
        {
            resetTimer -= Time.deltaTime;
            if(resetTimer <= 0)
            {
                if(player.IsKilled)
                {
                    SceneManager.LoadScene("GameMenu");
                } else
                {
                    SceneManager.LoadScene(levels.Pop());
                }
        
            }
        }
    }
}
