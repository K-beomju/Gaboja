using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private List<GameObject> battleList;

    private ObjectPooling<EnemyMove>[] enemyCreatePool;
    private readonly int stageCount = 4;
    public int enemyCount;

    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject battleScreen;

    public int stage;

    void Awake()
    {
        GameManager.Load("Enemy",enemyList);

        enemyCreatePool = new ObjectPooling<EnemyMove>[enemyList.Count];
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyCreatePool[i] = new ObjectPooling<EnemyMove>(enemyList[i], battleScreen.transform, 3);
        }

    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }


    public IEnumerator SpawnEnemy()
    {


        while(battleList.Count != enemyCount)
        {
             int rand = Random.Range(0,3);
            EnemyMove enemyMove = enemyCreatePool[rand].GetOrCreate();
            enemyMove.transform.position = spawnPos.position;
            enemyMove.RandSpeed();
            battleList.Add(enemyList[rand].gameObject);
            yield return Yields.WaitSeconds(0.1f);
            yield return null;
        }

        yield return null;
    }



    public EnemyMove GetCreateEnemy(int num)
    {
        return enemyCreatePool[num].GetOrCreate();
    }





}
