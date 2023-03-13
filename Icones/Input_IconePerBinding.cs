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
    //public InputActionReference InputBinding => inputBinding;
    //public bool Overrided { set => overrided = value; }
    public bool SetPropritzarMouse { set => prioritzaMouse = value; }

    //public InputActionReference SetInputActionReference(InputActionReference inputActionReference) => inputBinding = inputActionReference;
    public InputActionReference SetInputActionReference { set => inputBinding = value; }

    void OnEnable() 
    {
        if (inputBinding == null)
            return;

        MostrarIcone(inputBinding.action, overrided, prioritzaMouse);
    }



    public void MostrarIcone(bool forçar) 
    {
        if (forçar) trobat = false;
        MostrarIcone(inputBinding.action, overrided, prioritzaMouse);
    }
    public void MostrarIcone(InputActionReference inputActionReference, bool forçar = false) 
    {
        if (forçar) trobat = false;
        inputBinding = inputActionReference;
        MostrarIcone(inputActionReference.action, overrided, prioritzaMouse);
    } 

    [ContextMenu("Provar")]
    void Provar()
    {
        trobat = false;
        MostrarIcone(inputBinding.action, overrided, prioritzaMouse);
    }
}
