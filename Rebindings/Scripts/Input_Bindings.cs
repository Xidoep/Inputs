using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Inputs/Bindings", fileName = "Bindings")]
public class Input_Bindings : ScriptableObject
{
    [SerializeField] InputActionAsset inputActions;
    public static Input_Bindings Instance;
    [SerializeField] Input_BindingsGuardats guardats;

    private void OnEnable()
    {
        Debugar.Log("[Input_Bindings] OnEnable => Carregar()");
        Carregar();
    }
    public void Carregar()
    {
        if (guardats.Keys.Count == 0)
            return;

        for (int a = 0; a < inputActions.actionMaps.Count; a++)
        {
            foreach (var action in inputActions.actionMaps[a].actions)
            {
                for (int i = 0; i < action.bindings.Count; i++)
                {
                    for (int k = 0; k < guardats.Keys.Count; k++)
                    {
                        if(guardats.Keys[k] == action.actionMap + action.name + i)
                        {
                            action.ApplyBindingOverride(i, guardats.Values[k]);
                        }
                    }
                }
            }
        }
    }

    [ContextMenu("Guardar")]
    public void Guardar(InputAction action)
    {
        Debug.Log($"{action.bindings.Count} bindings");
        for (int i = 0; i < action.bindings.Count; i++)
        {
            Debugar.Log($"Binding - {action.actionMap + action.name + i}");
            if (!guardats.Keys.Contains(action.actionMap + action.name + i))
            {
                guardats.Add(action.actionMap + action.name + i, action.bindings[i].PathOrOverridePath(true));
            }
            else
            {
                guardats.Replace(action.actionMap + action.name + i, action.bindings[i].PathOrOverridePath(true));
            }
            Debugar.Log($"Guardar: {action.actionMap + action.name + i} amb valor {action.bindings[i].PathOrOverridePath(true)}");
        }
    }

    [ContextMenu("ResetBindings")]
    public void ResetBindings()
    {
        for (int a = 0; a < inputActions.actionMaps.Count; a++)
        {
            foreach (var action in inputActions.actionMaps[a].actions)
            {
                for (int i = 0; i < action.bindings.Count; i++)
                {
                    guardats.RemoveAll();
                }
            }
        }
        foreach (var item in inputActions)
        {
            item.RemoveAllBindingOverrides();

        }

    }

}


