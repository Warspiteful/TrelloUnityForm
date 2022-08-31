using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Variable/string",
    fileName = "NewStringVariable")]
public class StringVariable : ScriptableVariable<string>
{
    public override void ResetVal()
    {
        value = "";
    }
}
