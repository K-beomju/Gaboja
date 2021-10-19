using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : Observer
{
    private void Start()
    {
        type = UIType.Upgrade;
    }
    public override void Notify(Event type)
    {
        switch (type)
        {
            // 아이템 획득과 장비 변경때
            //case Event.Equipment:
            case Event.OnUpgrade: Debug.Log("인벤토리 UI를 업데이트 합니다."); break;
        }
    }



}
