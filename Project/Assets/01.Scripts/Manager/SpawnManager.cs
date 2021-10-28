using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
     public GameObject[] enemyType;
     public GameObject spawnPos;
     public int count; //에네미 마리수
    public  List<GameObject> SpawnEnemys = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine( SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {

        int selection = Random.Range(0, enemyType.Length);
        GameObject selectedObj = enemyType[selection];
        SpawnEnemys.Add(Instantiate(selectedObj, spawnPos.transform.position, Quaternion.identity));
    }

    public IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < count; i++)
        {
            Spawn();
        }
        yield return new WaitForSeconds(8f);
        StartCoroutine(SpawnEnemy());
    }
}
