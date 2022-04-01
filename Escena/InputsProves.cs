using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;
public class InputsProves : MonoBehaviour
{
    [Tooltip("This is automatically setted")][SerializeField] UI_Contextual contextual;
    [SerializeField] InputActionReference inputActionReference;
    [SerializeField] LayerMask layer;
    Collider[] colliders;
    [SerializeField] float radius;

    bool mostrat = false;
    int overlaps;

    private void OnEnable()
    {
        colliders = new Collider[1];
    }

    private void Update()
    {
        overlaps = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, layer);

        //colliders = XS_Physics.CollidersSphere(transform.position, radius);

        if (overlaps > 0) 
        {
            mostrat = true;
            contextual.Show(inputActionReference);
        }
        else if(overlaps == 0)
        {
            mostrat = false;
            contextual.Hide(inputActionReference);
        }
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
