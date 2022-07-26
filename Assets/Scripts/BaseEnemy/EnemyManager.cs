using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static List<string> EnemyList = new List<string>();
    public static List<string> RemovedEnemy = new List<string>();

    void Start()
    {
        StoreAndCheckEnemies();
    }

  
    void StoreAndCheckEnemies()
    {
        foreach (Transform child in transform)
        {
            foreach (string obj in RemovedEnemy)
            {
                if (child.gameObject.name == obj)
                {
                    child.gameObject.SetActive(false);
                }
            }

            if (child.gameObject.tag == "Enemy")
            {
                EnemyList.Add(child.gameObject.name);
            }
        }
    }
}
