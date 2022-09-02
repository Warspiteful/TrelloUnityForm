
using System;
using System.Collections;
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
    
    public TrelloAPI(string key, string token)
    {
        _key = key;
        _token = token;
    }
    
    private void CheckWebRequestStatus(string errorMessage, UnityWebRequest uwr) {
        
        //LMAO, My code only works with this in here right now
        Debug.Log(uwr.result);
        switch (uwr.result) {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.ProtocolError:
            case UnityWebRequest.Result.DataProcessingError:
                throw new Exception($"{errorMessage}: {uwr.error} ({uwr.downloadHandler.text})");
        }
    }

    public void Authenticate()
    {
      
        UnityWebRequest uwr = HandleRequest
            (
                UnityWebRequest.Get($"{MemberBaseUrl}?key={_key}&token={_token}")
            );

        while (!uwr.isDone)
        {
         
        }
        
    }
    public List<object> PopulateBoards()
    {
        UnityWebRequest uwr = HandleRequest
            (
                UnityWebRequest.Get($"{MemberBaseUrl}?key={_key}&token={_token}&boards=all")
            );
        
        var dict = Json.Deserialize(uwr.downloadHandler.text) as Dictionary<string,object>;
        uwr.Dispose();
        return (List<object>) dict["boards"];
    }

    public List<object> PopulateLists(string id)
    {
        UnityWebRequest uwr = HandleRequest
        (
            UnityWebRequest.Get(
            $"{BoardBaseUrl}/{id}/lists?key={_key}&token={_token}&boards=all")
        );
        
        List<object> listData = Json.Deserialize(uwr.downloadHandler.text) as List<object>;
        uwr.Dispose();
        return listData;
    }
    
    public List<object> PopulateLabels(string id)
    {
        UnityWebRequest uwr = HandleRequest
        (
            UnityWebRequest.Get(
                $"{BoardBaseUrl}/{id}/labels?key={_key}&token={_token}&boards=all")
        );
        
        List<object> labelData = Json.Deserialize(uwr.downloadHandler.text) as List<object>;
        uwr.Dispose();
        return labelData;
    }
    
    public void UploadCard(TrelloCard card)
    {
        var post = new WWWForm();
        post.AddField("name", card.name);
        post.AddField("desc", card.desc);
        post.AddField("due", card.due);
        post.AddField("idList", card.idList);
        post.AddField("idLabels", String.Join(",",card.labelIDList));
        
        UnityWebRequest uwr = HandleRequest
        (
            UnityWebRequest.Post($"{CardBaseUrl}?key={_key}&token={_token}", post)
        );
        
        uwr.Dispose();
    }
    
    private UnityWebRequest HandleRequest(UnityWebRequest uwr)
    {
        UnityWebRequestAsyncOperation operation = uwr.SendWebRequest();

        while (!operation.isDone)
        {
            CheckWebRequestStatus("Could not upload the Trello card.", uwr);
        }
        
        return uwr;
    }
}
