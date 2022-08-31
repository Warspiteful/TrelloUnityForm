using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrelloController : MonoBehaviour
{
    [Header("Trello Auth")] 
    [SerializeField] private string _key;
    [SerializeField] private string _token;

    [SerializeField] private StringListVariable _boardNames;
    [SerializeField] private StringListVariable _boardIDs;

    
    [Header("Trello Settings")]
    [SerializeField]
    private string _defaultBoard;
    [SerializeField]
    private string _defaultList;
    
    private void Start() {
        if (string.IsNullOrEmpty(_key) || string.IsNullOrEmpty(_token)) {
            throw new Exception("The Trello API key or token are missing!");
        }

        SetBoards();
    }
    public void SendNewCard(TrelloCard card, string list = null, string board = null) {

        if (board == null) {
            board = _defaultBoard;
        }
        if (list == null) {
            list = _defaultList;
        }

        StartCoroutine(AsyncSend(card, list, board));
    }
    
    public void SetBoards()
    {
        List<string> boards = new List<string>();
        List<string> boardsID = new List<string>();
        TrelloAPI api = new TrelloAPI(_key, _token);
        List<object> boardDict = api.PopulateBoards();
        foreach (Dictionary<string, object> board in boardDict)
        {
            boards.Add((string) board["name"]);
            boardsID.Add((string) board["id"]);
        }

        _boardNames.Value = boards;
        _boardIDs.Value = boardsID; 
    }

    IEnumerator AsyncSend(TrelloCard card, string list, string board)
    {
        TrelloAPI api = new TrelloAPI(_key, _token);
        
        yield return api.PopulateBoards();
        
        yield return null;
    }
}
