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

    private void Start()
    {
        SetBoards();
    }

    private void OnEnable()
    {
        activeBoardID.ValueUpdated += SetLists;
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
