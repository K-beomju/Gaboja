using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text;


[Serializable]
public class UiInfo
{
    public Text lvTxt;  // 레벨 텍스트
    public Text costDataText; // 업그레이드 비용 텍스트
    public Text nextDataTxt;  // 업그레이드 수치 텍스트
    public Button upgradeBtn; // 업그레이드 버튼
}


public class UpgradePanel : MonoBehaviour
{
    public UiInfo[] uiInfo;
    public PlayerHealth playerHealth;

    void Start()
    {
        Debug.Log(uiInfo.Length);
         for (int i = 0; i < uiInfo.Length; i++)
        {
            int temp = i;
            uiInfo[temp].upgradeBtn.onClick.AddListener(() => UpgradeData(uiInfo[temp] ,temp));
            SetText(uiInfo[temp] , temp);
        }
    }


    public void UpgradeData(UiInfo uiInfo ,int temp)
    {
        if (JsonSave.instance.GetDataClass().gold >=  JsonSave.instance.GetUpgradeClass().cost[temp])
        {
            Debug.Log("업그레이드 완료");

            JsonSave.instance.GetDataClass().gold -= JsonSave.instance.GetUpgradeClass().cost[temp];
            UiManager.Instance.SetGold();

             JsonSave.instance.GetUpgradeClass().cost[temp] += (int)Mathf.Round( JsonSave.instance.GetUpgradeClass().level[temp] + (JsonSave.instance.GetUpgradeClass().level[temp] + 1));
             JsonSave.instance.GetUpgradeClass().upAbility[temp] +=  JsonSave.instance.GetUpgradeClass().upCost[temp];
             JsonSave.instance.GetUpgradeClass().level[temp]++; // 레벨 증가

            SetText(uiInfo , temp);
            SetAbility(temp);

        }
    }

    public void SetText(UiInfo upgrade , int temp)
    {
        upgrade.lvTxt.text = string.Format("Lv.{0}", UiManager.Instance.GetDataText( JsonSave.instance.GetUpgradeClass().level[temp]));
       upgrade.costDataText.text = UiManager.Instance.GetDataText( JsonSave.instance.GetUpgradeClass().cost[temp]);
        upgrade.nextDataTxt.text = string.Format("{0} -> {1}", UiManager.Instance.GetDataText( JsonSave.instance.GetUpgradeClass().
        upAbility[temp] - JsonSave.instance.GetUpgradeClass().upCost[temp]),UiManager.Instance.GetDataText( JsonSave.instance.GetUpgradeClass().upAbility[temp]));

    }

    public void SetAbility(int temp )
    {
        switch(temp)
        {
            case 0:
                UiManager.Instance.SetAttack();
            break;

            case 1:
                playerHealth.SetFill();
            break;
        }
    }







}
