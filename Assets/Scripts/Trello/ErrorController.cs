using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorController : MonoBehaviour
{
    [Header("Error Panel")]
    [SerializeField]
    private GameObject ErrorPanelPrefab;
    private GameObject InstantiatedErrorPanel;

    [SerializeField] 
    private EventSignal showError;

    [SerializeField] 
    private EventSignal hideError;
    private void OnEnable()
    {
        showError.onCall += DisplayErrors;
        hideError.onCall += DestroyErrorPanel;
    }

    private void OnDisable()
    {
        showError.onCall -= DisplayErrors;
        hideError.onCall -= DestroyErrorPanel;
    }
    
    private void DisplayErrors()
    {
        InstantiatedErrorPanel = Instantiate(ErrorPanelPrefab, transform);
        InstantiatedErrorPanel.transform.SetAsFirstSibling();
    }
    
    private void DestroyErrorPanel()
    {
        Destroy(InstantiatedErrorPanel);
    }
}
