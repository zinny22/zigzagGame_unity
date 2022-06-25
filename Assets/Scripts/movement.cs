using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;        //움직이는 속도
    private Vector3 moveDirection;  //움직이는 방향

    public Vector3 MoveDirection => moveDirection;

    void Update()
    {
        transform.position += moveSpeed * moveDirection * Time.deltaTime;       //속도 * 방향 * 시간 = 위치 
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;          //입력된 방향을 움직이는 방향에 넣어주기
    }
}
