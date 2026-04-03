using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] VisualGameObjecArray;

    private void Start()
    {
        //这里订阅了Player脚本的OnSelectedCounterChanged事件
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    /// <summary>
    /// 这是事件回调函数，当选中计数器发生变化时，会调用这个函数
    /// </summary>
    /// <param name="sender">事件的发送者，这是player脚本过来的</param>
    /// <param name="e">计数器的引用</param>
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        //这里的e包含Player.OnSelectedCounterChangeEventArgs类型，里面包含了选中计数器的引用
        if (e.selectedCounter == baseCounter)
        {
            Show();
        } else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject VisualGameObject in VisualGameObjecArray)
        {
            VisualGameObject.SetActive(true);
        }
        
    }
    private void Hide()
    {
        foreach (GameObject VisualGameObject in VisualGameObjecArray)
        {
            VisualGameObject.SetActive(false);
        }
    }


}
