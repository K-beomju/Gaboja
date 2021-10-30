using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class GameManager : Singleton<GameManager>
{

    [Header("PoolObjs")]
    public List<GameObject> cavasEffect;
    public List<GameObject> screenEffect;


    private ObjectPooling<EffectObject>[] canvasEffectPool;
    private ObjectPooling<EffectObject>[] screenEffectPool;

    [Header("PoolTr")]
    [SerializeField]
    private RectTransform mainCanvas;


    protected override void Awake()
    {
        base.Awake();

        Load("Effect/Canvas", cavasEffect);
        Load("Effect/Screen", screenEffect);

        if (canvasEffectPool != null)
        {
            canvasEffectPool = new ObjectPooling<EffectObject>[cavasEffect.Count];
            for (int i = 0; i < cavasEffect.Count; i++)
            {
                canvasEffectPool[i] = new ObjectPooling<EffectObject>(cavasEffect[i], mainCanvas.transform, 3);
            }

        }

        if (screenEffectPool != null)
        {
            screenEffectPool = new ObjectPooling<EffectObject>[screenEffect.Count];
            for (int i = 0; i < cavasEffect.Count; i++)
            {
                screenEffectPool[i] = new ObjectPooling<EffectObject>(screenEffect[i], this.transform, 3);
            }
        }

    }
    public void Load(string subfolder, List<GameObject> list) // Load -> Casting -> List Add
    {
        object[] temp = Resources.LoadAll(subfolder);
        for (int i = 0; i < temp.Length; i++)
        {

            GameObject go = temp[i] as GameObject;
            list.Add(go);
        }
    }


    public static EffectObject GetCreateCanvasEffect(int num)
    {
        return Instance.canvasEffectPool[num].GetOrCreate();
    }

    public static EffectObject GetCreateScreenEffect(int num)
    {
        return Instance.screenEffectPool[num].GetOrCreate();
    }




}
