using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadSceneNext);
    }

    /// <summary>
    /// 跳转场景
    /// </summary>
    public void LoadSceneNext()
    {
        SceneManager.LoadSceneAsync(gameObject.name);
    }
}
