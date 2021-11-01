using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadMenu : Menu<LoadMenu>
{
    public Slider loadSlider;




    public void Active()
    {
        loadSlider.DOValue(1, 3f).OnComplete(() =>
        {
                        MenuManager.Instance.battleScreen.SetActive(true);

            MenuManager.Instance.mainCanvas.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        });
    }


}
