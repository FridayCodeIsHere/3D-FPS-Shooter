using UnityEngine;
using System;
using TMPro;
using Zenject;

[RequireComponent(typeof(InputManager))]
public abstract class Interactable : MonoBehaviour
{
    public Action OnEnterZone;
    public Action OnOutZone;
    public Action OnClick;
    
    public string PromptMessage;
    private TextMeshProUGUI _displayText;

    private void OnEnable()
    {
        OnEnterZone += ShowMessage;
        OnOutZone += HideMessage;
    }

    private void OnDisable()
    {
        OnEnterZone -= ShowMessage;
        OnOutZone -= HideMessage;
    }
    
    [Inject]
    private void Constructor(TextMeshProUGUI text)
    {
        _displayText = text;
    }

    public void BaseInteract()
    {
        
    }

    protected virtual void Interact()
    {
        
    }

    private void ShowMessage()
    {
        _displayText.text = PromptMessage;
    }

    private void HideMessage()
    {
        _displayText.text = string.Empty;
    }
    
}
