using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;





[Serializable]
public class PlayerDataInfo
{
    public float initHealth;


    public PlayerDataInfo()
    {
        initHealth = 1000;
    }
}


[Serializable]
public class EnemyDataInfo
{
    public float enemyDamage;
    public float bossDamage;

    public float enemyHp;

    public EnemyDataInfo()
    {
        enemyDamage = 1;
        bossDamage = 10;
        enemyHp = 26;
    }
}

[Serializable]
public class Upgrade
{
    public List<string> upName = new List<string>(); // 업그레이드 할 데이터 이름
    public List<int> level = new List<int>(); // 업그레이드 비용

    public List<float> upAbility = new List<float>();  // 업그레이드 수치 (저장)
    public List<float> upCost = new List<float>(); // 올라가는 능력치 고정
    public List<float> cost = new List<float>(); // 레벨

    public Upgrade()
    {
        //초기값은 하드코딩
        upName.Add("공격력 증가");
        level.Add(0);
        upAbility.Add(10);
        cost.Add(1);
        upCost.Add(10);

        upName.Add("체력 증가");
        level.Add(0);
        upAbility.Add(1000);
        cost.Add(1);
        upCost.Add(1000);


    }

}





[Serializable]
public class DataClass
{
    public Upgrade _upgrades = new Upgrade();
    public PlayerDataInfo _playerData = new PlayerDataInfo();
    public EnemyDataInfo _enemyData = new EnemyDataInfo();

    public int swordMax;                 // 검 제작 최대치
    public int swordCurrent;             // 검 보유 갯수

    public double gold;                         // 골드
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

        gold = 1132;
        dia = 32;
        emerald = 0;


        sortTime = 10;
        createSwordTime = 10;
        autoMergeTime = 20;
        autoCreateTime = 5;
        autoDiaMineTime = 20;
        autoEmeraldMineTime = 20;



    }

    public int CurrentSword()
    {
        return swordCurrent;
    }





}
