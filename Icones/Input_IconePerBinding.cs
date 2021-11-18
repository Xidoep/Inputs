using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

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

    [ContextMenu("Provar")]
    void Provar()
    {

        trobat = false;
        MostrarIcone(inputBinding.action);
    }
}
