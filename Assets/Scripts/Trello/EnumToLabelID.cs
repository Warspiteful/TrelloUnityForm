using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnumToLabel
{
    public SeverityEnum _severity;
    public string label;
}

public class EnumToLabelID : MonoBehaviour
{
    [SerializeField] private SeverityEnumVariable severity;

    [SerializeField] private StringVariable labelID;

    [SerializeField] private List<EnumToLabel> labelConversion;
    
    private void OnEnable()
    {
        severity.ValueUpdated += ConvertEnumToLabel;
    }

    private void OnDisable()
    {
        severity.ValueUpdated -= ConvertEnumToLabel;
    }

    private void ConvertEnumToLabel()
    {
        foreach(EnumToLabel converter in labelConversion)
        {
            if (severity.Value == converter._severity)
            {
                labelID.Value = converter.label;
                return;
            }
        }
        Debug.Log("Invalid Enum Provided");
    }
}
