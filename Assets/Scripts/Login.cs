using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Button gameBtn;
    public Button quitBtn;
    public Button resetBtn;

    private void Start()
    {
        gameBtn.onClick.AddListener(OnMenuSelect);
        quitBtn.onClick.AddListener(OnQuitBtn);
        resetBtn.onClick.AddListener(OnResetBtn);
    }

    private void OnMenuSelect()
    {
        SceneManager.LoadSceneAsync(SceneTags.MenuScene);
    }

    private void OnQuitBtn()
    {
        Application.Quit();
    }    
    
    private void OnResetBtn()
    {
        SystemManager.GetInstance().ClearAll();
    }
}
