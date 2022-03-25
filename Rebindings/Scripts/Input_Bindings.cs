using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Inputs/Bindings", fileName = "Bindings")]
public class Input_Bindings : ScriptableObject
{
    [SerializeField] Guardat guardat;
    [SerializeField] InputActionAsset inputActions;
    public static Input_Bindings Instance;
    [SerializeField] Input_BindingsGuardats guardats;

    private void OnEnable()
    {
        Debugar.Log("Input_Bindings - OnEnable => Carregar()");
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
                    //if (guardat.Existeix(action.actionMap + action.name + i))
                    //    action.ApplyBindingOverride(i, (string)guardat.Get(action.actionMap + action.name + i, null));

                    //if (guardats.Bindings.ContainsKey(action.actionMap + action.name + i))
                    //    action.ApplyBindingOverride(i, guardats.Bindings[action.actionMap + action.name + i]);

                    //if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                    //    action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));

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
            Debug.Log($"Binding - {action.actionMap + action.name + i}");
            //guardat.Set(action.actionMap + action.name + i, action.bindings[i].overridePath);
            if (!guardats.Keys.Contains(action.actionMap + action.name + i))
            {
                guardats.Add(action.actionMap + action.name + i, action.bindings[i].PathOrOverridePath());
            }
            else
            {
                guardats.Replace(action.actionMap + action.name + i, action.bindings[i].PathOrOverridePath());
            }
            Debug.Log($"Guardar: {action.actionMap + action.name + i} amb valor {action.bindings[i].PathOrOverridePath()}");
           

            //PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
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

                    //guardat.Borrar(action.actionMap + action.name + i);

                    //if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                    //    action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
                }
            }
        }
        foreach (var item in inputActions)
        {
            item.RemoveAllBindingOverrides();

        }
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteKey("rebinds");
        //guardat.Borrar("rebinds");
    }

}


