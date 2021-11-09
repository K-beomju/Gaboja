using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class GameManager : Singleton<GameManager>
{



    [Header("PoolObjs")]
    public List<GameObject> cavasEffect;
    public List<GameObject> screenEffect;
    public GameObject damageText;



    private ObjectPooling<EffectObject>[] canvasEffectPool;
    private ObjectPooling<EffectObject>[] screenEffectPool;
    private ObjectPooling<DamageText> damageTextPool;

    [Header("PoolTr")]
   [SerializeField] private RectTransform mainCanvas;
   [SerializeField] private RectTransform battleCanvas;


    protected override void Awake()
    {
        base.Awake();

        Load("Effect/Canvas", cavasEffect);
        Load("Effect/Screen", screenEffect);

        canvasEffectPool = new ObjectPooling<EffectObject>[cavasEffect.Count];
        for (int i = 0; i < cavasEffect.Count; i++)
        {
            canvasEffectPool[i] = new ObjectPooling<EffectObject>(cavasEffect[i], mainCanvas.transform, 3);
        }


        if (screenEffectPool == null)
        {
            screenEffectPool = new ObjectPooling<EffectObject>[screenEffect.Count];
            for (int i = 0; i < screenEffect.Count; i++)
            {
                screenEffectPool[i] = new ObjectPooling<EffectObject>(screenEffect[i], this.transform, 3);
            }
        }
        damageTextPool = new ObjectPooling<DamageText>(damageText, battleCanvas.transform , 10);


    }

    void Start()
    {
        // JsonSave.instance.generate();

    }



    public static void Load(string subfolder, List<GameObject> list) // Load -> Casting -> List Add
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

    public static DamageText GetDamageText()
    {
        return Instance.damageTextPool.GetOrCreate();
    }



    void OnApplicationQuit()
    {
        Debug.Log("게임 종료");
        JsonSave.instance.Save();
    }




}
