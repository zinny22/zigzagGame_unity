using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("GameStart UI")]
    [SerializeField]
    private FadeEffect[] fadeGameStart;

    [SerializeField]
    private GameObject PanelGameStart;

    public bool IsGameStart { private set; get; } = false;

    private IEnumerator Start()
    {
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
}
