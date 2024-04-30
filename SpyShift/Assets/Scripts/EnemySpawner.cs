using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float minX = -50;
    public float maxX = 50;
    public float minZ = -50;
    public float maxZ = 50;

    public GameObject enemyPre;
    private List<GameObject> enemys=new List<GameObject>();
    public int maxEnemyNum;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < maxEnemyNum; i++)
        {
            float posX = Random.Range(minX, maxX);
            float posZ = Random.Range(minZ, maxZ);
            Vector3 pos = new Vector3(posX, 1, posZ);
            GameObject go = Instantiate(enemyPre, pos, Quaternion.identity, transform);
            go.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            enemys.Add(go);
        }  
    }

    public GameObject GetNearestEnemy()
    {
        GameObject p = GameObject.Find("Player");
        float minDistance = float.MaxValue;
        GameObject nearestEnemy = enemys[0];
        for (int i = 0; i < enemys.Count; i++)
        {
            if (Vector3.Distance(p.transform.position,enemys[i].transform.position)<minDistance)
            {
                minDistance = Vector3.Distance(p.transform.position, enemys[i].transform.position);
                nearestEnemy = enemys[i];
            }
        }
        return nearestEnemy;
    }
}
