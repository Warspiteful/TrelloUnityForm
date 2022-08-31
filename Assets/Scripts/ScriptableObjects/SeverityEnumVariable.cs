using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Object Variable/SeverityEnum",
    fileName = "NewSeverityEnumVariable")]
public class SeverityEnumVariable : ScriptableVariable<SeverityEnum>
{
    public override void ResetVal()
    {
        value = SeverityEnum.LOW;
    }
}
