using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrelloModel : MonoBehaviour
{

    [Header("Authorization")]
    [SerializeField]
    private TrelloAuth authorization;

    [Header("Data Inputs")]
    [SerializeField]
    private StringVariable activeBoardID;

    [Header("Event Signal Inputs")]
    [SerializeField]
    private EventSignal Initialized;

    [Header("Event Signal Output")] 
    [SerializeField]
    private EventSignal boardsLoaded;
    [SerializeField]
    private EventSignal listsLoaded;

    [Header("Data Outputs")]
    [SerializeField]
    private StringListVariable _boardNames;
    [SerializeField]
    private StringListVariable _boardIDs;
    [SerializeField]
    private StringListVariable _listNames;
    [SerializeField]
    private StringListVariable _listIDs;
    [SerializeField]
    private StringListVariable _labelNames;
    [SerializeField]
    private StringListVariable _labelIDs;


    private void OnEnable()
    {
        activeBoardID.ValueUpdated += SetLists;
        activeBoardID.ValueUpdated += SetLabels;

        Initialized.onCall += SetBoards;
    }
    
    private void OnDisable()
    {
        activeBoardID.ValueUpdated -= SetLists;
        activeBoardID.ValueUpdated -= SetLabels;

        Initialized.onCall -= SetBoards;
    }
    
    public void SetBoards()
    {
        List<string> boards = new List<string>();
        List<string> boardsID = new List<string>();
        TrelloAPI api = new TrelloAPI(authorization.key, authorization.token);
        List<object> boardDict = api.PopulateBoards();
        foreach (Dictionary<string, object> board in boardDict)
        {
            boards.Add((string) board["name"]);
            boardsID.Add((string) board["id"]);
        }
        _boardIDs.Value = boardsID; 
        _boardNames.Value = boards;
        
        boardsLoaded.onCall?.Invoke();

    }

    public void SetLabels()
    {
        List<string> labelNames = new List<string>();
        List<string> labelIDs = new List<string>();
        TrelloAPI api = new TrelloAPI(authorization.key, authorization.token);
        List<object> labelDict = api.PopulateLabels(activeBoardID.Value);
        foreach (Dictionary<string, object> board in labelDict)
        {
            labelNames.Add((string) board["name"]);
            labelIDs.Add((string) board["id"]);
        }

        _labelNames.Value = labelNames;
        _labelIDs.Value = labelIDs;

    }

    public void SetLists()
    {
        List<string> lists = new List<string>();
        List<string> listID = new List<string>();
        TrelloAPI api = new TrelloAPI(authorization.key, authorization.token);
        List<object> listDict = api.PopulateLists(activeBoardID.Value);
        foreach (Dictionary<string, object> board in listDict)
        {
            lists.Add((string) board["name"]);
            listID.Add((string) board["id"]);
        }

        _listNames.Value = lists;
        _listIDs.Value = listID;
        
        listsLoaded.onCall?.Invoke();
    }

    
}
