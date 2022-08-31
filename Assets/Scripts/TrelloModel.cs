using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrelloModel
{
    private StringListVariable _boardNames;
    private StringListVariable _boardIDs;
    private StringListVariable _listNames;
    private StringListVariable _listIDs;

    public TrelloModel(
        StringListVariable boardNames,
        StringListVariable boardIDs,
        StringListVariable listNames,
        StringListVariable listIDs)
    {
        _boardNames = boardNames;
        _boardIDs = boardIDs;
        _listNames = listNames;
        _listIDs = listIDs;
    }
    
    
}
