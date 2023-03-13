using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

public class Input_IconePerBinding : Input_Icone
{
    [Space(10)]
    [SerializeField] InputActionReference inputBinding;
    [SerializeField] InputDevice device;
    [SerializeField] bool prioritzaMouse = false;
    [SerializeField] bool overrided;
    public InputActionReference InputBinding => inputBinding;
    public bool Overrided { set => overrided = value; }

    void OnEnable() 
    {
        if (inputBinding == null)
            return;

        MostrarIcone(inputBinding.action, overrided, prioritzaMouse);
    }
    

    
    public void MostrarIcone() => MostrarIcone(inputBinding.action, overrided, prioritzaMouse);
    public void MostrarIcone(InputActionReference inputActionReference) => MostrarIcone(inputActionReference.action, overrided, prioritzaMouse);

    [ContextMenu("Provar")]
    void Provar()
    {
        trobat = false;
        MostrarIcone(inputBinding.action, overrided, prioritzaMouse);
    }
}
