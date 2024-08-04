using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    public Transform[] spwanPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;
    

    void Awake()
    {
        spwanPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / 10f),spawnData.Length-1);

        if( timer > (spawnData[level].spawnTime))
        {
            timer = 0;
            SpawnEnemy ();
        }
    }
    void SpawnEnemy()
    {
        GameObject enermy = GameManager.Instance.pool.Get(0);
        enermy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
        enermy.GetComponent<Enermy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}