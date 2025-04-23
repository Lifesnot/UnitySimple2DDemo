using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public GameObject hitPrefab;
    
    //碰撞开始
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.COIN)
        {
            UIManager.GetInstance().SetCount();
            Destroy(collision.gameObject);
        }

        if (collision.tag == Tags.TARGET)
        {
            UIManager.GetInstance().ShowWinPanel();
            UIManager.GetInstance().isGameEnd = true;
        }

        if (collision.tag == Tags.WALL)
        {
            Instantiate(hitPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            UIManager.GetInstance().ShowGameOver();
        }

        if (collision.tag == Tags.KEY)
        {
            collision.gameObject.SetActive(false);
            collision.transform.parent.GetComponent<Door>().OpenDoor();
        }
    }

    //碰撞结束
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
