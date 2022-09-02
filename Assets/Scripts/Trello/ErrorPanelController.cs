using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPanelController : MonoBehaviour
{
    [SerializeField] private StringListVariable errors;

    [SerializeField] private TMP_Text errorText;

    private VerticalLayoutGroup verticalLayoutGroup;
    private ContentSizeFitter _sizeFitter;


    private void Awake()
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        _sizeFitter = GetComponent<ContentSizeFitter>();
    }

    private void OnEnable()
    {
        RenderErrors();
    }

    private void OnDisable()
    {
        errors.Value.Clear();
    }

    private void OnDestroy()
    {
        errors.Value.Clear();
    }

    private void RenderErrors()
    {
        Debug.Log("Error Check");
        
        
        string errorString = "There was an error with your submission:\n";
        foreach (string error in errors.Value)
        {
            Debug.Log(error);
            errorString += "* " + error + "\n";
        }

        errorString += "Please Retry";

        errorText.text = errorString;

        StartCoroutine(UpdateRect());

    }
    
    IEnumerator UpdateRect()
    {
        if (verticalLayoutGroup != null)
        {
            verticalLayoutGroup.enabled = false;
            yield return new WaitForSeconds(0.1F);
            verticalLayoutGroup.enabled = true;
            yield return new WaitForSeconds(0.1F);
            _sizeFitter.enabled = false;
            yield return new WaitForSeconds(0.1F);
            _sizeFitter.enabled = true;
        }
        else
        {
            Debug.Log("BRUH");
        }
    }
}
