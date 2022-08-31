using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object Variable/stringList",
    fileName = "NewStringListVariable")]
public class StringListVariable : ScriptableVariable<List<string>>
{
    public override void ResetVal()
    {
        
    }
}
