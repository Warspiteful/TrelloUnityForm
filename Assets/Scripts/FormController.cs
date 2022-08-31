using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormController : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private StringListVariable options;
    [SerializeField] private StringListVariable optionsIDs;
    [SerializeField] private StringVariable selectedBoard;
    [SerializeField] private StringVariable selectedBoardID;


    private void OnEnable()
    {
        options.ValueUpdated += AddDropdownOptions;
    }

    private void OnDisable()
    {
        options.ValueUpdated -= AddDropdownOptions;
    }

    void AddDropdownOptions() {
        if (options.Value.Count != optionsIDs.Value.Count)
        {
            Debug.Log("List Error! Options and IDs aren't synced");
            return;
        }
        dropdown.ClearOptions ();
        dropdown.AddOptions(options.Value);
    }

    public void SetActiveBoard(Int32 selectedDropdown)
    {
        selectedBoard.Value = options.Value[selectedDropdown];
        selectedBoardID.Value = optionsIDs.Value[selectedDropdown];
    }
}
