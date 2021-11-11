using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerDataInfo
{
    public float hp;

    public PlayerDataInfo()
    {
        hp = 200;
    }
}


[Serializable]
public class EnemyDataInfo
{
    public float enemyDamage;
    public float bossDamage;

    public EnemyDataInfo()
    {
        enemyDamage = 1;
        bossDamage = 10;
    }
}


[Serializable]
public class DataClass
{
    public EnemyDataInfo enemyData = new EnemyDataInfo();


    public int swordMax;                 // 검 제작 최대치
    public int swordCurrent;             // 검 보유 갯수

    public int gold;                         // 골드
    public int dia;                          // 보석
    public int emerald;

    public int sortTime;                   // 자석 쿨타임
    public int createSwordTime;            // 검 제작 시간
    public int autoMergeTime;              // 자석 쿨타임
    public int autoCreateTime;             // 모루 쿨타임
    public int autoDiaMineTime;            // 다이아 채굴 쿨타임
    public int autoEmeraldMineTime;        // 에메랄드 채굴 쿨타임




    public int version = 1; // 세이브 파일 버전

     public DataClass()
    {
        swordMax = 5;
        swordCurrent = 5;

        gold = 0;
        dia = 0;
        emerald = 0;


        sortTime = 10;
        createSwordTime = 10;
        autoMergeTime = 20;
        autoCreateTime = 20;
        autoDiaMineTime = 20;
        autoEmeraldMineTime = 20;

    }





}
