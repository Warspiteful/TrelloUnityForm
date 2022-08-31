using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormController : MonoBehaviour
{

    [Header("Card")]
    [SerializeField] private TrelloCard card;
    
    [Header("UI Elements")]
    [SerializeField] private TMP_Dropdown BoardDropdown;
    [SerializeField] private TMP_Dropdown ListDropdown;
    
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

    [Header("Input Event Signal")] 
    [SerializeField]
    private EventSignal onBoardLoad;
    [SerializeField]
    private EventSignal onListLoad;

    [Header("Output Event Signal")] 
    [SerializeField]
    private EventSignal sendSignal;

    
    private void OnEnable()
    {
        onBoardLoad.onCall += AddBoardDropdownOptions;
        onListLoad.onCall += AddListDropdownOptions;
    }

    private void OnDisable()
    {
        onBoardLoad.onCall -= AddBoardDropdownOptions;
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
    }

    private void AddListDropdownOptions()
    {
        ToggleListDropdown(true);
        ListDropdown.ClearOptions();
        ListDropdown.AddOptions(lists.Value);
        selectedList.Value = "";
        selectedListId.Value = "";
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
                     "h" + "\n";
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
    
    
    public void SetActiveList(Int32 selectedDropdown)
    {
        selectedList.Value = lists.Value[selectedDropdown];
        selectedListId.Value = listIds.Value[selectedDropdown];
    }

    public void SendSignal()
    {
        sendSignal.onCall?.Invoke();
    }
}
