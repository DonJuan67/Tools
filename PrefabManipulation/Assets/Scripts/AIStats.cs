using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Stats")]
public class AIStats : ScriptableObject
{
    public float hp;
    public float speed;

    public void Save()
    {
        //Look up and implement
        //UnityEditor.AssetDatabase.CreateAsset 
    }
}
