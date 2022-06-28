using System.Collections;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("GameStart UI")]
    [SerializeField]
    private FadeEffect[] fadeGameStart;
    [SerializeField]
    private GameObject PanelGameStart;
    [SerializeField]
    private TextMeshProUGUI textGameStartBestScore;

    [Header("InGame UI")]
    [SerializeField]
    private TextMeshProUGUI textInGameScore;

    [Header("GameOver UI")]
    [SerializeField]
    private GameObject PanelGameOver;
    [SerializeField]
    private TextMeshProUGUI textGameOverScore;
    [SerializeField]
    private float timeStopTime;
    [SerializeField]
    private GameObject PanelScore;
    [SerializeField]
    private TextMeshProUGUI textGameOverBestScore;

    public bool IsGameStart { private set; get; } = false;
    public bool IsGameOver { private set; get; } = false;
    private int currentScore = 0;

    private IEnumerator Start()
    {
        Time.timeScale = 1;

        int bestScore = PlayerPrefs.GetInt("BestScore");
        textGameStartBestScore.text = bestScore.ToString();

        for( int i =0; i<fadeGameStart.Length; ++i)
        {
            fadeGameStart[i].FadeIn();
        }
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
                IsGameStart = true;
                yield break;
            }
            yield return null;
        }
    }

    public void GameStart()
    {
        PanelGameStart.SetActive(false);
        textInGameScore.gameObject.SetActive(true);
    }

    public void IncreaseScore(int score = 1)
    {
        currentScore += score;
        textInGameScore.text = currentScore.ToString();
    }

    public void GameOver()
    {
        IsGameOver = true;

        textGameOverScore.text = currentScore.ToString();
        //textGameOverScore.gameObject.SetActive(false);

        PanelGameOver.SetActive(true);
        PanelScore.SetActive(true);

        int bestScore = PlayerPrefs.GetInt("BestScore");
        if(currentScore> bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            textGameOverBestScore.text = currentScore.ToString();
        }
        else
        {
            textGameOverBestScore.text = bestScore.ToString();
        }
        StartCoroutine("SlowAndStopTime");
    }

    private IEnumerator SlowAndStopTime()
    {
        float current = 0;
        float percent = 0;

        Time.timeScale = 0.5f;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / timeStopTime;
            yield return null;
        }

        Time.timeScale = 0; 

    }
}
