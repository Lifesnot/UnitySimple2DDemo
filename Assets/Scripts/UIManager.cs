using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : BaseMonoManager<UIManager>
{
    private GameObject gameOver;
    private GameObject winPanel;

    private Button closeBtn;
    private Button gameOverMenuBtn;

    private Button winBtn;
    private Button winMenButton;

    private Text countText;
    private int total;

    public bool isGameEnd;

    private void Start()
    {
        gameOver = transform.Find("GameOver").gameObject;
        winPanel = transform.Find("WinPanel").gameObject;

        var gameOverTrs = gameOver.transform;
        var winPanelTrs = winPanel.transform;
        closeBtn = gameOverTrs.Find("Close").GetComponent<Button>();
        gameOverMenuBtn = gameOverTrs.Find("Menu").GetComponent<Button>();
        winBtn = winPanelTrs.Find("Win").GetComponent<Button>();
        winMenButton = winPanelTrs.Find("Menu").GetComponent<Button>();
        closeBtn.onClick.AddListener(OnClose);
        gameOverMenuBtn.onClick.AddListener(OnMenu);
        winBtn.onClick.AddListener(OnNext);
        winMenButton.onClick.AddListener(OnMenu);

        countText = transform.Find("Count").GetComponent<Text>();
        countText.text = "0";
    }

    /// <summary>
    /// 显示失败面板
    /// </summary>
    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    /// <summary>
    /// 显示胜利面板
    /// </summary>
    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    /// <summary>
    /// 重置当前游戏
    /// </summary>
    public void OnClose()
    {
        //Debug.Log("重新开始当前游戏");
        int id = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(id);
    }

    /// <summary>
    /// 回到菜单界面
    /// </summary>
    public void OnMenu()
    {
        //Debug.Log("回到菜单界面");
        SceneManager.LoadSceneAsync("MenuScene");
    }

    /// <summary>
    /// 下一个场景关卡
    /// </summary>
    public void OnNext()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string[] nameList = sceneName.Split("Level");
        int curIndex = int.Parse(nameList[1]);
        string nextScene = "Level" + (curIndex + 1);
        SceneManager.LoadSceneAsync(nextScene);
        // 存储下个关卡解锁进度
        SystemManager.GetInstance().SetSystem(nextScene, 1);
    }

    /// <summary>
    /// 设置分数
    /// </summary>
    public void SetCount()
    {
        total++;
        countText.text = total.ToString();
    }
}
