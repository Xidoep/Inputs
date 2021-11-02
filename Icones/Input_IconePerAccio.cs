using UnityEngine;
using UnityEngine.InputSystem;

public class Input_IconePerAccio : Input_Icone
{
    [Space(10)]
    public InputAction accio;

    private void OnEnable()
    {
        MostrarIcone(accio);
    }

    private void OnValidate()
    {
        MostrarIcone(accio);
    }

}
