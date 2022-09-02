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
    private StringVariable listId;
    [SerializeField]
    private StringVariable dueTime;
    [SerializeField]
    private StringVariable labelID;
    [SerializeField]
    private StringVariable firstName;
    [SerializeField] 
    private StringVariable lastName;
    [SerializeField] 
    private StringVariable email;

    [Header("Error Data")] 
    [SerializeField] 
    private StringListVariable errorList;
    
    [Header("Trello Settings")]
    [SerializeField]
    private string _defaultBoard;
    [SerializeField]
    private string _defaultList;

    [Header("Input Signals")]
    [SerializeField]
    private EventSignal sendSignal;
    
    [Header("Ouput Signals")]
    [SerializeField]
    private EventSignal resetSignal;
    [SerializeField] 
    private EventSignal alertErrors;


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
        

        card.name = title.Value;
        
        card.desc = "#" + "Demo " + Application.version + "\n" +
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
                    desc.Value + "\n" +
                    "```\n" +
                    "___\n" +
                    "###Other Information\n" +
                    "Playtime: " +
                    String.Format("{0:0}:{1:00}", Mathf.Floor(Time.time / 60), Time.time % 60) +
                    "h\n" + 
                    "___\n" +
                    "###User Contact\n" +
                    "- Name: " + firstName.Value + " " + lastName.Value + "\n" +
                    "- Email: " + email.Value + "\n";


        card.due = dueTime.Value;
        
        TrelloAPI api = new TrelloAPI(authorization.key, authorization.token);

        card.labelIDList.Add(labelID.Value);

        card.idList = listId.Value;


        List<string> errors = new List<string>();

        try
        {
            api.UploadCard(card);
        }
        catch (Exception e)
        {
            errors.Add(e.Message);
            errorList.Value = errors;
            alertErrors.onCall?.Invoke();
        }
        resetSignal.onCall?.Invoke();

        yield return null;
    }
}
