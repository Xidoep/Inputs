using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[SelectionBase]
public class Input_EsdevenimentPerBinding : MonoBehaviour
{
    public InputActionReference[] escoltadors;
    public UnityEvent OnInteractuar;


    private void OnEnable()
    {
        for (int i = 0; i < escoltadors.Length; i++)
        {
            escoltadors[i].action.performed += Interactuar;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < escoltadors.Length; i++)
        {
            escoltadors[i].action.performed -= Interactuar;
        }
    }



    public void Interactuar(InputAction.CallbackContext context)
    {
        Debug.Log($"Interactuar desde {this.gameObject.name}");
        if (context.phase == InputActionPhase.Performed) OnInteractuar.Invoke();
    }

}
