using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[SelectionBase]
public class Input_EsdevenimentPerBinding : MonoBehaviour
{
    public InputActionReference escoltador;
    public UnityEvent OnInteractuar;


    private void OnEnable()
    {
        escoltador.action.performed += Interactuar;
    }
    private void OnDisable()
    {
        escoltador.action.performed -= Interactuar;
    }



    public void Interactuar(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) OnInteractuar.Invoke();
    }

}
