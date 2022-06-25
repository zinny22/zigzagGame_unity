using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("GameStart UI")]
    [SerializeField]
    private FadeEffect[] fadeGameStart;
    [SerializeField]
    private GameObject PanelGameStart;

    [Header("GameOver UI")]
    [SerializeField]
    private GameObject PanelGameOver;
    [SerializeField]
    private float timeStopTime;

    public bool IsGameStart { private set; get; } = false;
    public bool IsGameOver { private set; get; } = false;

    private IEnumerator Start()
    {
        Time.timeScale = 1;

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
    }

    public void GameOver()
    {
        IsGameOver = true;
        PanelGameOver.SetActive(true);
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
