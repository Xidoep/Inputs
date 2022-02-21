using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using XS_Utils;

[SelectionBase]
public class Input_EsdevenimentPerBinding : MonoBehaviour
{
    [SerializeField] InputActionReference[] escoltadors;
    [SerializeField] UnityEvent OnInteractuar;
    [SerializeField] bool multipleInteractions = false;
    bool interacted = false;

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
        if (interacted)
        {
            Debugar.Log("It has been interacted once and have multipleInteractions value sets as false");
            return;
        }

        Debugar.Log($"Interactuar desde {this.gameObject.name}");
        if (context.phase == InputActionPhase.Performed) OnInteractuar.Invoke();


        if (!multipleInteractions)
            interacted = true;
    }

    

}
