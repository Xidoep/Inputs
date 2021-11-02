using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Input_ReasignarBindings_UI : MonoBehaviour
{
    [SerializeField] InputActionReference inputActionReference;
    [SerializeField] bool excludeMouse = true;
    [Range(0, 10)]
    [SerializeField] private int selectedBinding;
    [SerializeField] InputBinding.DisplayStringOptions displayStringOptions;

    [Header("Binding Info - NO NOT EDIT")]
    [SerializeField] InputBinding inputBinding;
    int bindingIndex;

    string actionName;

    [Header("UI Fields")]
    [SerializeField] Text actionText;
    [SerializeField] Button rebindButton;
    [SerializeField] Text rebindText;
    [SerializeField] Button resetButton;

    private void OnEnable()
    {
        rebindButton.onClick.AddListener(() => DoRebind());
        resetButton.onClick.AddListener(() => ResetBinding());

        if (inputActionReference != null)
        {
            Input_ReasignarBindings.Carregar(actionName);
            GetBindingInfo();
            UpdateUI();
        }

        Input_ReasignarBindings.rebindComplete += UpdateUI;
        Input_ReasignarBindings.rebindCanceled += UpdateUI;
    }

    private void OnDisable()
    {
        Input_ReasignarBindings.rebindComplete -= UpdateUI;
        Input_ReasignarBindings.rebindCanceled -= UpdateUI;
    }


    private void OnValidate()
    {
        if (inputActionReference == null)
            return;

        GetBindingInfo();
        UpdateUI();
    }


    void GetBindingInfo()
    {
        if (inputActionReference.action != null)
        {
            actionName = inputActionReference.action.name;
        }

        if (inputActionReference.action.bindings.Count > selectedBinding)
        {
            inputBinding = inputActionReference.action.bindings[selectedBinding];
            bindingIndex = selectedBinding;
        }
    }

    void UpdateUI()
    {
        if (actionText != null)
        {
            actionText.text = actionName;
        }

        if (rebindText != null)
        {
            if (Application.isPlaying)
            {
                rebindText.text = Input_ReasignarBindings.GetBindingName(actionName, bindingIndex);
            }
            else
            {
                rebindText.text = inputActionReference.action.GetBindingDisplayString(bindingIndex, displayStringOptions);
            }
        }
    }

    private void DoRebind()
    {
        Input_ReasignarBindings.StartRebind(actionName, bindingIndex, rebindText, excludeMouse);
    }

    private void ResetBinding()
    {
        Input_ReasignarBindings.Resetejar(actionName, bindingIndex);
        UpdateUI();
    }
}
