using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PoolData", menuName = "Scriptable Object/PoolData", order = int.MaxValue)]
public class PoolData : ScriptableObject
{
    [Header("Pooling")]
    // 풀 오브젝트
    [SerializeField]
    private GameObject sword;
    public GameObject Sword { get { return sword; } }


    [Header("Parent")]
    // 풀 오브젝트 위치
    [SerializeField]
    private Canvas uiCanvas;
    public Canvas UiCanvas { get { return uiCanvas; } }

    [Header("ObjectPool")]
    public ObjectPooling<MergeItem> swordPool;


    public void Init(Transform transform)
    {
        swordPool = new ObjectPooling<MergeItem>(sword,transform);
    }




}
