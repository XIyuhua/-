using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]//为脚本对象提供一个创建菜单项，可以方便地创建新的实例
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite Sprite;
    public string objectName;
    
   
}
