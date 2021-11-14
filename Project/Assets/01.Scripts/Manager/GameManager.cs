using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class GameManager : Singleton<GameManager>
{



    [Header("PoolObjs")]
    public List<GameObject> cavasEffect;
    public List<GameObject> screenEffect;
    public List<GameObject> swordEffect;
    public GameObject damageText;
    public GameObject dropGold;



    private ObjectPooling<EffectObject>[] canvasEffectPool;
    private ObjectPooling<EffectObject>[] screenEffectPool;
    private ObjectPooling<EffectObject>[] swordEffectPool;
    private ObjectPooling<DamageText> damageTextPool;
    private ObjectPooling<DropGold> dropGoldPool;

    [Header("PoolTr")]
   [SerializeField] private RectTransform mainCanvas;
   [SerializeField] private RectTransform battleCanvas;
    [SerializeField] private Transform battleScreen;

   public static bool isAttack = false;
   [SerializeField] private BackGroundMove backGroundMove;



    protected override void Awake()
    {
        base.Awake();

        Load("Effect/Canvas", cavasEffect);
        Load("Effect/Screen", screenEffect);
        Load("Effect/SwordEffect" , swordEffect);

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

        if(swordEffectPool == null)
        {
            swordEffectPool = new ObjectPooling<EffectObject>[swordEffect.Count];
            for (int i = 0; i < swordEffect.Count; i++)
            {
                swordEffectPool[i] = new ObjectPooling<EffectObject>(swordEffect[i], battleScreen.transform, 10);
            }
        }


        damageTextPool = new ObjectPooling<DamageText>(damageText, battleCanvas.transform , 10);
        dropGoldPool = new ObjectPooling<DropGold>(dropGold, battleScreen.transform , 10);


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

    public void BackMove(bool isAttack)
    {
        backGroundMove.isAttack = isAttack;
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

    public static DropGold GetDropGold()
    {
        return Instance.dropGoldPool.GetOrCreate();
    }

    public static EffectObject GetSwordEffect(int num)
    {
        return Instance.swordEffectPool[num].GetOrCreate();
    }



    void OnApplicationQuit()
    {
        Debug.Log("게임 종료");
        JsonSave.instance.Save();
    }




}
