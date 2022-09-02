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

    [SerializeField] 
    private StringListVariable errorList;
    
    
    [Header("Data Outputs")]
    [SerializeField] private StringVariable selectedBoard;
    [SerializeField] private StringVariable selectedBoardId;
    [SerializeField] private StringVariable selectedList;
    [SerializeField] private StringVariable selectedListId;

    [SerializeField] private StringVariable desc;
    [SerializeField] private StringVariable title;
    [SerializeField] private StringVariable firstName;
    [SerializeField] private StringVariable lastName;
    [SerializeField] private StringVariable email;
    [SerializeField] private StringVariable dueTime;
    [SerializeField] private SeverityEnumVariable severity;
    
    [Header("Input Event Signal")] 
    [SerializeField]
    private EventSignal onBoardLoad;
    [SerializeField]
    private EventSignal onListLoad;
    [SerializeField]    
    private EventSignal showError;
    [SerializeField] 
    private EventSignal hideError;
    [SerializeField] 
    private EventSignal displaySignal;


    [Header("Output Event Signal")] 
    [SerializeField]
    private EventSignal sendSignal;

    [SerializeField]
    private EventSignal resetSignal;

    [Header("Controlled Game Objects")]
    [SerializeField]
    private GameObject Canvas;
    
    
    
    private void OnEnable()
    {
  
        onBoardLoad.onCall += AddBoardDropdownOptions;
        onListLoad.onCall += AddListDropdownOptions;
        resetSignal.onCall += ResetFields;
        displaySignal.onCall += ToggleScreen;

    }

    private void OnDisable()
    {
        onBoardLoad.onCall -= AddBoardDropdownOptions;
        onListLoad.onCall -= AddListDropdownOptions;
        resetSignal.onCall -= ResetFields;
        displaySignal.onCall -= ToggleScreen;

    }

    private void ToggleScreen()
    {
        Canvas.SetActive(!Canvas.activeSelf);

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

    public void SetDescription(string text)
    {
        desc.Value = text;
    }
    
    public void SetFirstName(string text)
    {
        firstName.Value = text;
    }
    public void SetLastName(string text)
    {
        lastName.Value = text;
    }
    
    public void SetEmail(string text)
    {
        email.Value = text;
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
        dueTime.Value = DateTime.Today.ToString();

        hideError.onCall?.Invoke();

        if(ValidateFields()){
            sendSignal.onCall?.Invoke();
        }
        else
        {
            showError.onCall?.Invoke();
        }
    }

    private bool ValidateFields()
    {
        List<string> errorList = new List<string>();

        if (string.IsNullOrEmpty(firstName.Value))
        {
            errorList.Add("Must enter First Name.");
        }
        if (string.IsNullOrEmpty(lastName.Value))
        {
            errorList.Add("Must enter Last Name.");
        }
        if (string.IsNullOrEmpty(email.Value))
        {
            errorList.Add("Must enter Email.");
        }
        if (string.IsNullOrEmpty(title.Value))
        {
            errorList.Add("Must enter Title.");
        }

        if (string.IsNullOrEmpty(desc.Value))
        {
            errorList.Add("Must Enter Description");
        }
        if (string.IsNullOrEmpty(selectedBoard.Value) || string.IsNullOrEmpty(selectedBoardId.Value))
        {
            errorList.Add("Must enter Title.");
        }
        if (string.IsNullOrEmpty(selectedList.Value) || string.IsNullOrEmpty(selectedListId.Value))
        {
            errorList.Add("Must enter Title.");
        }

        this.errorList.Value = errorList;
            
        return errorList.Count == 0;

    }

    private void ResetFields()
    {
        SetActiveBoard(0);
        BoardDropdown.value = 0;
        TitleInput.text = "";
        DescInput.text = "";
        FirstNameInput.text = "";
        LastNameInput.text = "";
        EmailInput.text = "";
    }



    public void SetSeverity(SeverityValue severityVal)
    {
     
        severity.Value = severityVal.severity;
    }
    
}
