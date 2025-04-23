using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : BaseMonoManager<SystemManager>
{
    public void SetSystem(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }
    
    public void SetSystem(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }
    
    public void SetSystem(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
    }

    public int GetSystem(string name)
    {
        return PlayerPrefs.GetInt(name);
    }
    
    public T GetInfo<T>(object obj)
    {
        return (T)obj;
    }

    public void ClearAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
