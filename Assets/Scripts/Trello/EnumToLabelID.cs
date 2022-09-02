using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnumToLabel
{
    public SeverityEnum _severity;
    public int label;
}

public class EnumToLabelID : MonoBehaviour
{
    [SerializeField] private SeverityEnumVariable severity;

    [SerializeField] private StringVariable labelID;

    [SerializeField] private List<EnumToLabel> labelConversion;
    
    [SerializeField] private StringListVariable labels;
    [SerializeField] private StringListVariable labelIds;
    [SerializeField] private StringVariable selectedLabel;
    [SerializeField] private StringVariable selectedLabelId;
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
                selectedLabel.Value = labels.Value[converter.label];
                selectedLabelId.Value = labelIds.Value[converter.label];
                return;
            }
        }
        Debug.Log("Invalid Enum Provided");
    }
}
