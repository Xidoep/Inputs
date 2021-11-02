using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Input_EsdevenimentPerAccio : MonoBehaviour
{
    public InputAction accio;
    public UnityEvent OnInteractuar;

    private void OnEnable()
    {
        accio.performed += Interactuar;
    }
    private void OnDisable()
    {
        accio.performed -= Interactuar;
    }



    public void Interactuar(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) OnInteractuar.Invoke();
    }
}
