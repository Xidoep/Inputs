using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

public class Input_IconePerBinding : Input_Icone
{
    [Space(10)]
    [SerializeField] InputActionReference inputBinding;
    [SerializeField] InputDevice device;
    public InputActionReference InputBinding => inputBinding;



    void OnEnable() 
    {
        if (inputBinding == null)
            return;

        MostrarIcone(inputBinding.action);
    }
    

    
    public void MostrarIcone() => MostrarIcone(inputBinding.action);
    public void MostrarIcone(InputActionReference inputActionReference) => MostrarIcone(inputActionReference.action);

    [ContextMenu("Provar")]
    void Provar()
    {
        trobat = false;
        MostrarIcone(inputBinding.action);
    }
}
