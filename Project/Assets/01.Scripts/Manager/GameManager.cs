using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private PoolData poolData; // 풀링 데이터

    [SerializeField]
    private GameObject poolStorage; // 풀링 저장소



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        poolData.Init(this.transform);

    }



    public MergeItem GetCreateSword()
    {
        return instance.poolData.swordPool.GetOrCreate();
    }




}
