using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrelloController : MonoBehaviour
{
    [Header("Authorization")]
    [SerializeField]
    private TrelloAuth authorization;

    [Header("Card Information")]
    [SerializeField]
    private StringVariable title;
    [SerializeField]
    private StringVariable desc;
    [SerializeField] 
    private StringVariable idList;
    
    [Header("Trello Settings")]
    [SerializeField]
    private string _defaultBoard;
    [SerializeField]
    private string _defaultList;

    [Header("Input Signals")]
    [SerializeField]
    private EventSignal sendSignal;

    private void OnEnable()
    {
        sendSignal.onCall += SendNewCard;
    }

    private void OnDisable()
    {
        sendSignal.onCall -= SendNewCard;
    }

    private void Start() {
        if (string.IsNullOrEmpty(authorization.key) || string.IsNullOrEmpty(authorization.token))
        {
            throw new Exception("The Trello API key or token are missing!");
        }
    }
    public void SendNewCard() {
        StartCoroutine(AsyncSend());
    }
    


    IEnumerator AsyncSend()
    {
        TrelloCard card = new TrelloCard();
        if (string.IsNullOrEmpty(idList.Value))
        {
            card.idList = _defaultList;
        }
        else
        {
            card.idList = idList.Value;
        }
        card.name = title.Value;
        card.desc = desc.Value;
        card.due = DateTime.Today.ToString();
        
        TrelloAPI api = new TrelloAPI(authorization.key, authorization.token);

        yield return api.UploadCard(card);

    }
}
