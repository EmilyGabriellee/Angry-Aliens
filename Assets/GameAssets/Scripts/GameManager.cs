using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Condição de vitória
    public static int EnemiesAlive = 0;

    [Header("Game Over")]
    public static bool GameIsOver;
    [SerializeField] GameObject gameOverUI;

    [Header("Complete Levels")]
    [SerializeField] GameObject completeLevelUI;
    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(EnemiesAlive);

        //Fim de jogo
        if (GameIsOver)
        {
            return;
        }

        //Win condition
        if (EnemiesAlive <= 0)
        {
            WinLevel();
        }
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        // SceneManager.LoadScene(SceneManager.GetNextScene().buildIndex);   
    }

    public void EndGame()
    {
        GameIsOver = true;
        Debug.Log("Game Over!");
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;
        Debug.Log("Next Level Unlocked");
        completeLevelUI.SetActive(true);
    }
}
