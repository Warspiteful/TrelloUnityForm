using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Dropdown BoardDropdown;
    [SerializeField] private TMP_Dropdown ListDropdown;
    [SerializeField] private TMP_InputField TitleInput;
    [SerializeField] private TMP_InputField DescInput;
    
    [SerializeField] private TMP_InputField FirstNameInput;

    [SerializeField] private TMP_InputField LastNameInput;

    [SerializeField] private TMP_InputField EmailInput;

    [Header("Data Inputs")]
    [SerializeField] private StringListVariable boards;
    [SerializeField] private StringListVariable boardIds;
    [SerializeField] private StringListVariable lists;
    [SerializeField] private StringListVariable listIds;

    [Header("Data Outputs")]
    [SerializeField] private StringVariable selectedBoard;
    [SerializeField] private StringVariable selectedBoardId;
    [SerializeField] private StringVariable selectedList;
    [SerializeField] private StringVariable selectedListId;
    [SerializeField] private StringVariable desc;
    [SerializeField] private StringVariable title;
    [SerializeField] private StringVariable dueTime;
    [SerializeField] private SeverityEnumVariable severity;
    
    [Header("Input Event Signal")] 
    [SerializeField]
    private EventSignal onBoardLoad;
    [SerializeField]
    private EventSignal onListLoad;

    [Header("Output Event Signal")] 
    [SerializeField]
    private EventSignal sendSignal;

    [SerializeField]
    private EventSignal resetSignal;
    
    
    
    private void OnEnable()
    {
  
        onBoardLoad.onCall += AddBoardDropdownOptions;
        onListLoad.onCall += AddListDropdownOptions;
        resetSignal.onCall += ResetFields;
    }

    private void OnDisable()
    {
        onBoardLoad.onCall -= AddBoardDropdownOptions;
        onListLoad.onCall -= AddListDropdownOptions;
        resetSignal.onCall -= ResetFields;
    }

    private void AddBoardDropdownOptions() {
        if (boards.Value.Count != boardIds.Value.Count)
        {
            Debug.Log("List Error! Options and IDs aren't synced");
            return;
        }

        List<string> options = boards.Value.Prepend(" ").ToList();
        BoardDropdown.ClearOptions();
        BoardDropdown.AddOptions(options);
        SetActiveBoard(0);
    }

    private void AddListDropdownOptions()
    {
        ToggleListDropdown(true);
        ListDropdown.ClearOptions();
        ListDropdown.AddOptions(lists.Value);
        SetActiveList(0);
    }
    
    private void ToggleListDropdown(bool isRevealed)
    {
        ListDropdown.gameObject.SetActive(isRevealed);
    }

    public void SetDescription(string description)
    {
        desc.Value = "#" + "Demo " + Application.version + "\n" +
                     "___\n" +

                     "###System Information\n" +
                     "- " + SystemInfo.operatingSystem + "\n" +
                     "- " + SystemInfo.processorType + "\n" +
                     "- " + SystemInfo.systemMemorySize + " MB\n" +
                     "- " + SystemInfo.graphicsDeviceName + " (" + SystemInfo.graphicsDeviceType +
                     ")\n" +
                     "\n" +
                     "___\n" +
                     "###User Description\n" +
                     "```\n" +
                     description + "\n" +
                     "```\n" +
                     "___\n" +
                     "###Other Information\n" +
                     "Playtime: " +
                     String.Format("{0:0}:{1:00}", Mathf.Floor(Time.time / 60), Time.time % 60) +
                     "h\n" + 
                     "___\n" +
                     "###User Contact\n" +
                     "- Name: " + FirstNameInput.text + " " + LastNameInput.text + "\n" +
                     "- Email: " + EmailInput.text + "\n";
    }

    public void SetActiveBoard(Int32 selectedDropdown)
    {
        if (selectedDropdown == 0)
        {
            ToggleListDropdown(false);
            return;
        }
        selectedBoard.Value = boards.Value[selectedDropdown - 1];
        selectedBoardId.Value = boardIds.Value[selectedDropdown - 1];
    }

    public void SetTitle(string title)
    {
        this.title.Value = DateTime.Today + " - " + title;
    }
    
    public void SetActiveList(Int32 selectedDropdown)
    {
        selectedList.Value = lists.Value[selectedDropdown];
        selectedListId.Value = listIds.Value[selectedDropdown];
    }

    public void SendSignal()
    {
        if (string.IsNullOrEmpty(selectedListId.Value))
        {
            Debug.Log("Must select Valid List ID");
        }

        if (string.IsNullOrEmpty(desc.Value))
        {
            Debug.Log("Must Add Description");
        }

        if (string.IsNullOrEmpty(title.Value))
        {
            Debug.Log("Must place title");
        }
        
        dueTime.Value = DateTime.Today.ToString();
        
        sendSignal.onCall?.Invoke();
    }

    private void ResetFields()
    {
        SetActiveBoard(0);
        BoardDropdown.value = 0;

        TitleInput.text = "";
        DescInput.text = "";
    }

    public void SetSeverity(SeverityValue severityVal)
    {
        severity.Value = severityVal.severity;
    }
}
