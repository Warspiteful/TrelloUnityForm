
using System;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;
using UnityEngine.Networking;

/*
 * TrelloAPI.cs
 * Interact directly with the Trello API using MiniJSON and uploads cards. 
 * 
 * Original by bfollington
 * https://github.com/bfollington/Trello-Cards-Unity 
 * and by Àdam Carballo
 * https://github.com/AdamCarballo/Unity-Trello
 */

public class TrelloAPI
{

    private const string MemberBaseUrl = "https://api.trello.com/1/members/me";
    private const string BoardBaseUrl = "https://api.trello.com/1/boards/";
    private const string CardBaseUrl = "https://api.trello.com/1/cards/";
    
    private string _key;
    private string _token;
    

    private List<object> _boards;
    private List<object> _lists;
    private string _currentBoardId;
    private string _currentListId;

    
    public TrelloAPI(string key, string token)
    {
        _key = key;
        _token = token;
    }
    
    private void CheckWebRequestStatus(string errorMessage, UnityWebRequest uwr) {
        switch (uwr.result) {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.ProtocolError:
            case UnityWebRequest.Result.DataProcessingError:
                throw new Exception($"{errorMessage}: {uwr.error} ({uwr.downloadHandler.text})");
        }
    }

    public List<object> PopulateBoards()
    {
        _boards = null;
        UnityWebRequest uwr = UnityWebRequest.Get($"{MemberBaseUrl}?key={_key}&token={_token}&boards=all");
        UnityWebRequestAsyncOperation operation = uwr.SendWebRequest();

        while (!operation.isDone) {
            Debug.Log("Waiting");
            CheckWebRequestStatus("The Trello servers did not respond.", uwr);
        }
        
        var dict = Json.Deserialize(uwr.downloadHandler.text) as Dictionary<string,object>;
      
        _boards = (List<object>) dict["boards"];
        foreach (Dictionary<string, object> board in _boards)
        {
            Debug.Log((string) board["name"]);
        }
        return _boards;
    }

    public List<object> PopulateLists(string id)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(
            $"{BoardBaseUrl}/{id}/lists?key={_key}&token={_token}&boards=all");
        Debug.Log(uwr.url);
        UnityWebRequestAsyncOperation operation = uwr.SendWebRequest();

        while (!operation.isDone) {
            
            CheckWebRequestStatus("The Trello servers did not respond.", uwr);
        }
        Debug.Log(uwr.downloadHandler.text);
        
        _lists = Json.Deserialize(uwr.downloadHandler.text) as List<object>;
        
        foreach (Dictionary<string, object> list in _lists)
        {
            Debug.Log((string) list["name"]);
        }
        
        

        
        return _lists;
    }

    public void SetCurrentBoard(string name)
    {
        if (_boards == null) {
            throw new Exception("There are no boards available. Either the user does not have access to a board or PopulateBoards() wasn't called.");
        }
        
        foreach (Dictionary<string, object> board in _boards)
        {
            if ((string) board["name"] != name) continue;
            _currentBoardId = (string) board["id"];
            return;
        }
        
        _currentBoardId = null;
        throw new Exception($"A board with the name {name} was not found.");
    }

    public void SetCurrentList(string name)
    {
    }

    public string GetCurrentListId()
    {
        return "";
    }

    public TrelloCard UploadCard(TrelloCard card)
    {
        var post = new WWWForm();
        post.AddField("name", card.name);
        post.AddField("desc", card.desc);
        post.AddField("due", card.due);
        post.AddField("idList", card.idList);
        
        var uwr = UnityWebRequest.Post($"{CardBaseUrl}?key={_key}&token={_token}", post);
        var operation = uwr.SendWebRequest();
        
        while (!operation.isDone) {
            CheckWebRequestStatus("Could not upload the Trello card.", uwr);
        }

        Debug.Log($"Trello card sent!\nResponse {uwr.responseCode}");
        return card;
    }


}
