using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Input_ReasignarBindings : MonoBehaviour
{
    public static InputActionAsset inputActions;

    public static event Action rebindComplete;
    public static event Action rebindCanceled;
    public static event Action<InputAction, int> rebindStarted;

    private void Awake()
    {
        if(inputActions == null)
        {
            inputActions = PlayerInput.GetPlayerByIndex(0).actions;
        }
        //PlayerInput.GetPlayerByIndex(0).actions;
    }

    public static void StartRebind(string actionName, int bindingIndex, Text statusText, bool excludeMouse)
    {
        InputAction action = inputActions.FindAction(actionName);
        if(action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Couldn't find action or binding");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            var firstPartIndex = bindingIndex + 1;
            if(firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isComposite)
            {
                DoRebind(action, bindingIndex, statusText, true, excludeMouse);
            }
        }
        else
        {
            DoRebind(action, bindingIndex, statusText, false, excludeMouse);
        }
    }

    //Pot ser publica i utilitzarla directament
    static void DoRebind(InputAction actionToRebind, int bidningIndex, Text statusText, bool allCompositeParts, bool excludeMouse) 
    {
        Debug.Log(actionToRebind.bindings[bidningIndex].path);

        if (actionToRebind == null || bidningIndex < 0)
            return;

        statusText.text = $"Press a {actionToRebind.expectedControlType}";

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(bidningIndex);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            if (allCompositeParts)
            {
                var nextBindingIndex = bidningIndex + 1;
                if(nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                {
                    DoRebind(actionToRebind, nextBindingIndex, statusText, allCompositeParts, excludeMouse);
                }
            }

            Guardar(actionToRebind);
            rebindComplete?.Invoke();
        });

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            rebindCanceled?.Invoke();
        });

        rebind.WithCancelingThrough("<Keyboard>/escape");

        if (excludeMouse)
            rebind.WithControlsExcluding("Mouse");

        rebindStarted?.Invoke(actionToRebind, bidningIndex);
        rebind.Start(); //Actually start the rebind process
    }

    public static string GetBindingName(string actionName, int bindingIndex)
    {
        if(inputActions == null)
        {
            inputActions = PlayerInput.GetPlayerByIndex(0).actions;
        }

        InputAction action = inputActions.FindAction(actionName);
        return action.GetBindingDisplayString(bindingIndex);
    }

    private static void Guardar(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }

    [ContextMenu("Carregar")]
    public static void Carregar(string actionName)
    {
        if (inputActions == null)
        {
            inputActions = PlayerInput.GetPlayerByIndex(0).actions;
        }

        InputAction action = inputActions.FindAction(actionName);

        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
        }
    }

    public static void Resetejar(string actionName, int bindingIndex)
    {
        InputAction action = inputActions.FindAction(actionName);

        if(action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Could not find action or binding");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            for (int i = bindingIndex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
            {
                action.RemoveBindingOverride(i);
            }
        }
        else
        {
            action.RemoveBindingOverride(bindingIndex);
        }

        Guardar(action);
    }
}
