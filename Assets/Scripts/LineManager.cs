using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum PlayerState
{
    Idle,
    Run,
    Attack,
}

public class LineManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    
    // 记录划线坐标点
    private List<Vector2> pointList = new List<Vector2>();

    private bool isRun;
    
    private Transform playerTrs;

    private int currentIndex = 0;

    [SerializeField]
    private float speed;

    private Animator animator;

    private PlayerState state;

    [SerializeField]
    private float timer = 2f;

    private bool gameOver;

    private bool canDraw;
    
    private Vector3 faceRight = Vector3.zero;
    private Vector3 faceLeft = new Vector3(0, 180 ,0);

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        playerTrs = GameObject.Find("Player").transform;
        animator = playerTrs.GetComponent<Animator>();
        state = PlayerState.Idle;
    }

    private void Update()
    {
        DrawLineStart();

        SetMouseUp();

        DrawLineEnd();

        SetPlayerState();
    }

    /// <summary>
    /// 设置鼠标抬起事件 
    /// </summary>
    private void SetMouseUp()
    {
        // 鼠标左键抬起
        if (Input.GetMouseButtonUp(0))
        {
            isRun = true;
            canDraw = true;
            state = PlayerState.Run;
        }
    }

    /// <summary>
    /// 设置人物动画状态
    /// </summary>
    private void SetPlayerState()
    {
        switch (state)
        {
            case PlayerState.Idle:
                animator.SetBool(PlayAniStateTag.IsRun, false);
                break;
            case PlayerState.Run:
                animator.SetBool(PlayAniStateTag.IsRun, true);
                break;
        }
    }

    /// <summary>
    /// 画线开始
    /// </summary>
    private void DrawLineStart()
    {
        // 鼠标左键点击
        if (Input.GetMouseButton(0) && !canDraw)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!pointList.Contains(position))
            {
                pointList.Add(position);
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
            }
        }
    }

    /// <summary>
    /// 画线结束
    /// </summary>
    private void DrawLineEnd()
    {
        if (isRun && !gameOver)
        {
            playerTrs.position = Vector3.MoveTowards(playerTrs.position, pointList[currentIndex], Time.deltaTime * speed);
            if (Vector3.Distance(playerTrs.position, pointList[currentIndex]) < 0.1f)
            {
                //判断朝向
                if (pointList[currentIndex].x > playerTrs.position.x)
                {
                    playerTrs.localEulerAngles = faceRight;
                }
                else
                {
                    playerTrs.localEulerAngles = faceLeft;
                }
                currentIndex++;
                if (currentIndex >= pointList.Count)
                {
                    currentIndex = pointList.Count - 1;
                    isRun = false;
                    state = PlayerState.Idle;

                    if (!gameOver)
                    {
                        StartCoroutine(IsGameOver());
                    }
                    else
                    {
                        StopCoroutine(IsGameOver());
                    }
                }
            }
        }
    }

    /// <summary>
    /// 协程使用
    /// </summary>
    /// <returns></returns>
    IEnumerator IsGameOver()
    {
        yield return new WaitForSeconds(timer);
        if (!UIManager.GetInstance().isGameEnd)
        {
            UIManager.GetInstance().ShowGameOver();
            gameOver = true;
        }
    }
}