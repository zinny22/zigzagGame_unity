using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    private movement Movement;
    private float limitDeathY;
    private Animator animator;
    private BoxCollider boxCollider;

    [SerializeField]
    private Vector3 touchBeganPos;              //터치 시작 위치 
    private Vector3 touchEndedPos;              //터치 종료 위치 
    private Vector3 touchDif;                   //둘의 차이를 나타내줄 변수 값 
    private float swipeSensitivity;             //스와이프 민감도

    //점프 버튼을 입력 받았는지 알기 위한 변수
    bool jDown;
    //현재 점프 중인지 알기 위한 변수
    bool isJump;
    //움직이는 벡터?
    Vector3 moveVec;
    //플레이어의 Rigidbody를 받아오기 위한 변수
    Rigidbody rigid;

    private void Awake()
    {
        Movement = GetComponent<movement>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        //Movement.MoveTo(Vector3.forward);
        limitDeathY = transform.position.y - transform.localScale.y * 0.5f;
        isJump = false;
        rigid = GetComponent<Rigidbody>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if(gameController.IsGameStart == true)
            {
                Movement.MoveTo(Vector3.forward);

                yield break;
            }
            yield return null;
        };

    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.IsGameOver == true) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchBeganPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                touchEndedPos = touch.position;
                touchDif = (touchEndedPos - touchBeganPos);

                //스와이프. 터치의 x이동거리나 y이동거리가 민감도보다 크면
                if (Mathf.Abs(touchDif.y) > swipeSensitivity || Mathf.Abs(touchDif.x) > swipeSensitivity)
                {
                    if (touchDif.y > 0 && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("up");
                        jDown = true;
                        Jump();
                    }
                    else if (touchDif.x > 0 && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("right");
                        Vector3 direction = Vector3.right;
                        Movement.MoveTo(direction);
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                        gameController.IncreaseScore();

                    }
                    else if (touchDif.x < 0 && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("Left");
                        Vector3 direction = Vector3.forward;
                        Movement.MoveTo(direction);
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        gameController.IncreaseScore();
                    }
                }
                //터치.
                else
                {
                    Debug.Log("touch");
                }
            }
        }

        if( transform.position.y < limitDeathY)
        {
            gameController.GameOver();
        }
    }

    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump)
        {
            rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isJump = false;
    }

}