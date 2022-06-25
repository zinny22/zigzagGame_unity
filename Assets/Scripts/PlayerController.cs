using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector3 moveDirection;

    public Vector3 MoveDirection => moveDirection;

    [SerializeField]
    private Vector3 touchBeganPos;              //터치 시작 위치 
    private Vector3 touchEndedPos;              //터치 종료 위치 
    private Vector3 touchDif;                   //둘의 차이를 나타내줄 변수 값 
    private float swipeSensitivity;             //스와이프 민감도 

    private void Awake()
    {
        MoveTo(Vector3.forward);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
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
                    if (touchDif.y < 0 && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("down");
                    }
                    else if (touchDif.x > 0 && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("right");
                        Vector3 direction = Vector3.right;
                        transform.rotation = Quaternion.Euler(0, 90, 0);
                        MoveTo(direction);
                    }
                    else if (touchDif.x < 0 && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("Left");
                        Vector3 direction = Vector3.forward;
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        MoveTo(direction);
                    }
                }
                //터치.
                else
                {
                    Debug.Log("touch");
                }
            }
        }

    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}