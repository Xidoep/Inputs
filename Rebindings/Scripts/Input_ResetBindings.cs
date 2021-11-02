using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_ResetBindings : MonoBehaviour
{

    [SerializeField] InputActionAsset inputActions;

    public void ResetBindings()
    {
        foreach (var item in inputActions)
        {
            item.RemoveAllBindingOverrides();
        }
        PlayerPrefs.DeleteKey("rebinds");
        //guardat.Borrar("rebinds");
    }

}
