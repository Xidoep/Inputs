using UnityEngine;
using UnityEngine.InputSystem;

public class Input_IconePerBinding : Input_Icone
{
    [Space(10)]
    [SerializeField] InputActionReference inputBinding;

    public InputActionReference InputBinding => inputBinding;

    private void OnEnable()
    {
        MostrarIcone(inputBinding.action);
    }

    public void MostrarIcone()
    {
        MostrarIcone(inputBinding.action);
    }

    /*private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        MostrarIcone(inputBinding.action);
    }*/
}
