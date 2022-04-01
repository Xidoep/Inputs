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
    [Tooltip("If this is set as FALSE, a flanc triggers when the action if performed, and prevent to interact it again. Unless you disable and re-enable the object again")][SerializeField] bool multipleInteractions = false;
    [Tooltip("It delays the registration on action performed on Enable")][SerializeField] float delayRegistration = 0;
    
    bool interacted = false;
    bool registrated = false;

    private void OnEnable()
    {
        interacted = false;
        registrated = false;
        if (delayRegistration == 0)
        {
            RegistrateInteraction();
            return;
        }

        StartCoroutine(RegistrateInteractionDelayed());
    }

    private void OnDisable()
    {
        UnregistrateInteration();
    }





    void Interactuar(InputAction.CallbackContext context)
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





    void RegistrateInteraction()
    {
        for (int i = 0; i < escoltadors.Length; i++)
        {
            escoltadors[i].action.performed += Interactuar;
        }
        registrated = true;
    }
    IEnumerator RegistrateInteractionDelayed()
    {
        yield return new WaitForSeconds(delayRegistration);
        RegistrateInteraction();
    }



    void UnregistrateInteration()
    {
        if (!registrated)
            return;

        for (int i = 0; i < escoltadors.Length; i++)
        {
            escoltadors[i].action.performed -= Interactuar;
        }
    }



    private void OnValidate()
    {
        if (delayRegistration < 0) delayRegistration = 0;
    }

}
