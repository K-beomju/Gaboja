using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Pooling Objs")]
    [SerializeField]
    private GameObject sword;
    public Canvas poolObj;

    private ObjectPooling<MergeItem> swordPool;


    private void Awake()
    {
        instance = this;
        swordPool = new ObjectPooling<MergeItem>(sword, poolObj.transform, 5);
    }

    public MergeItem GetCreateSword()
    {
        return instance.swordPool.GetOrCreate();
    }

    
}
