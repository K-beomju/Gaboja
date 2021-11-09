using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleLogin : MonoBehaviour
{
    public void Login()
    {
        GPGSBuilder.Inst.Login((a, b) =>
         {
             LoadMenu.Open();
             LoadMenu.Instance.Active();
         });
    }
}
