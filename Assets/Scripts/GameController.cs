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
    private GameObject PanelGameScore;
    [SerializeField]
    private TextMeshProUGUI textInGameScore;
    [SerializeField]
    private TextMeshProUGUI textInGameCoinScore;
    private Animator animator;

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
    [SerializeField]
    private TextMeshProUGUI textGameOverCoinScore;

    public bool IsGameStart { private set; get; } = false;
    public bool IsGameOver { private set; get; } = false;
    private int currentScore = 0;
    private int currentCoin = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
                animator.SetBool("start", true);
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
        PanelGameScore.SetActive(true);

    }

    public void IncreaseScore(int score = 1)
    {
        currentScore += score;
        textInGameScore.text = currentScore.ToString();
    }

    public void IncreaseCoin(int coin = 2)
    {
        currentCoin += coin;
        textInGameCoinScore.text = currentCoin.ToString();
    }

    public void GameOver()
    {
        IsGameOver = true;

        textGameOverScore.text = currentScore.ToString();
        textGameOverCoinScore.text = currentCoin.ToString();

        PanelGameOver.SetActive(true);
        PanelScore.SetActive(true);
        PanelGameScore.SetActive(false);
        animator.SetBool("over", true);

        int bestScore = PlayerPrefs.GetInt("BestScore");

        if(currentScore > bestScore)
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
