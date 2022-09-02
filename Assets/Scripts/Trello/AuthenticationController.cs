using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class AuthenticationController : MonoBehaviour
{
    private TrelloAPI api;

    [SerializeField] private TrelloAuth auth;

    [SerializeField] private StringListVariable _errorList;

    [SerializeField] private EventSignal alertError;
    [SerializeField] private EventSignal hideError;
    [SerializeField] private EventSignal loggedIn;

    [SerializeField] private GameObject loginScreen;

    public void SetToken(string text)
    {
        auth.token = text;
    }
    
    public void SetKey(string text)
    {
        auth.key = text;
    }
    
    public void TryLogin()
    {
        hideError.onCall?.Invoke();
        
        api = new TrelloAPI(auth.key, auth.token);
        
        try
        {
            api.Authenticate();
            loggedIn.onCall?.Invoke();
            loginScreen.SetActive(false);

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            _errorList.Value = new List<string> {"Invalid Authentication"};
            alertError.onCall?.Invoke();
            loginScreen.SetActive(true);
        }
    }







}
