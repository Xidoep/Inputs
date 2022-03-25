using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

public class InputsProves : MonoBehaviour
{
    [SerializeField] UI_Contextual contextual;
    [SerializeField] InputActionReference inputActionReference;
    [SerializeField] Collider[] colliders;
    bool mostrat;
    private void OnTriggerEnter(Collider other)
    {
        contextual.Show(inputActionReference);
    }

    private void OnTriggerExit(Collider other)
    {
        contextual.Hide(inputActionReference);
    }

    private void Update()
    {
        colliders = XS_Physics.CollidersSphere(transform.position, 3);

        if (colliders.Length > 0 && !mostrat) 
        {
            mostrat = true;
            contextual.Show(inputActionReference);
        }
        else if(colliders.Length == 0 && mostrat)
        {
            mostrat = false;
            contextual.Hide(inputActionReference);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawSphere(transform.position, 3f);
    }
}
