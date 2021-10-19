using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//옵저버 패턴,
//UI뿐만 아니라 데이터를 일괄로 전달하기 위해 자주 사용하는 패턴
//UI가 오픈 되어 있을 때에만 해당 UI에 데이터를 전달
public class Observer : MonoBehaviour
{
    public Observer nextOb = null;
    public UIType type;
    // 옵저버를 상속받는 클래스들은 다음 함수를 꼭 구현해야 한다
    public virtual void Notify(Event type)
    {
        // 데이터가 전달 되었을 때 수행해야 하는 일들을 진행할 수 있다
    }

}
