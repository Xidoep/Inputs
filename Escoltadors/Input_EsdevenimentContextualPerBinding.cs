using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using XS_Utils;

/// <summary>
/// This register an event to an input action when enter the trigger, and fires that event when the input is performed.
/// </summary>
public class Input_EsdevenimentContextualPerBinding : MonoBehaviour
{
    [Tooltip("This is automatically setted")] [SerializeField] UI_Contextual contextual;
    [Space(20)]
    [SerializeField] InputActionReference inputActionReference;
    [SerializeField] LocalizedString localizedString;
    [Space(10)]
    [SerializeField] LayerMask layer;
    [SerializeField] UnityEvent OnInteractuar;
    Collider[] colliders;
    [SerializeField] float radius;

    int overlaps;

    private void OnEnable()
    {
        colliders = new Collider[1];
    }
    private void Update()
    {
        overlaps = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, layer);

        if (overlaps > 0)
        {
            contextual.Show(inputActionReference, localizedString);
            inputActionReference.action.performed += Interactuar;
        }
        else if (overlaps == 0)
        {
            inputActionReference.action.performed -= Interactuar;
            contextual.Hide(inputActionReference);
        }
    }

    void Interactuar(InputAction.CallbackContext context)
    {
        Debugar.Log($"Interactuar desde {this.gameObject.name}");
        if (context.phase == InputActionPhase.Performed) OnInteractuar.Invoke();
    }






    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .5f);
        Gizmos.DrawSphere(transform.position, radius);
    }
    private void OnValidate()
    {
        contextual = XS_Editor.LoadAssetAtPath<UI_Contextual>("Assets/XidoStudio/Inputs/Contextual/Basics/Contextual.asset");
    }
}
