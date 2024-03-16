using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CharaMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask solidObjects;
    [SerializeField] LayerMask Tilemap;
    //[SerializeField] GameController gameController;
    //相互依存を解消するためにUnityActionを用いて関数を登録する↑のGameControllerよりいい。
    public UnityAction OnEncounted;

    bool isMoving;

    Vector2 input;
    Animator animator;

    //壁判定のLayer
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void HandleUpdate()
    {
        if(!isMoving)
        {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        //斜め移動禁止
        if(input.x != 0)
        {
            input.y = 0;
        }

        if(input != Vector2.zero)
        {
        animator.SetFloat("moveX",input.x);
        animator.SetFloat("moveY",input.y);
        Vector2 targetPos = transform.position;
        targetPos += input;
        if(IsWalkable(targetPos))
        {
            StartCoroutine(Move(targetPos));
        }
        }
    }
    animator.SetBool("isMoving",isMoving);
    }
    
//コルーチンを使って徐々に目的地に近づける
    IEnumerator Move(Vector3 targetPos)
    {
        //移動中は入力を受け付けたくない
        isMoving = true;
        //targetPosとの差があるなら繰り返す
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed*Time.deltaTime
                );
                yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
        CheckForEncounter();
    }

    //targetPosに移動可能かを調べる関数
    bool IsWalkable(Vector2 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos,0.2f,solidObjects)== false;
    }

    void CheckForEncounter()
    {
        if(Physics2D.OverlapCircle(transform.position,0.2f,Tilemap))
        {
            if(Random.Range(0,100) < 7)
            {
                Debug.Log("モンスターに遭遇");
                animator.SetBool("isMoving",false);
                //GameController.StartBattle();
                OnEncounted();
            }
        }
    }
}