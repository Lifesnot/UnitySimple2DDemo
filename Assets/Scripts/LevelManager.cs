using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button backBtn;
    
    private void Awake()
    {
        // 1的话可以运行当前场景， -1就是没有解锁
        SystemManager.GetInstance().SetSystem("Level1", 1);
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.AddComponent<SelectItem>();
            Button childBtn = child.GetComponent<Button>();
            Text childText = child.GetComponentInChildren<Text>();
            
            int level = i + 1;
            string levelName = "Level" + level;
            child.name = levelName;
            childText.text = level.ToString();

            if (PlayerPrefs.HasKey(levelName))
            {
                Debug.Log("我存在："+levelName);
            }
            else
            {
                childBtn.interactable = false;
                childText.color = Color.black;
            }
        }
        backBtn.onClick.AddListener(OnBackBtn);
    }

    private void OnBackBtn()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
