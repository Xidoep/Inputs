using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

public class Input_IconePerBinding : Input_Icone
{
    [Apartat("BINDING")]
    [SerializeField] InputActionReference inputBinding;

    [Apartat("OPCIONS")]
    [SerializeField] bool prioritzaMouse = false;
    [SerializeField] bool overrided;
    //[Nota("Afageix un Tipus si vols que sempre utilizi aquell Input i ignori el reconeixment dinamic. Per una UI de controls, per exemple.", NoteType.Warning)]
    [SerializeField] Input_ReconeixementTipus tipusForçat;

    //[SerializeField] InputDevice device;
    //public InputActionReference InputBinding => inputBinding;
    //public bool Overrided { set => overrided = value; }
    public bool SetPropritzarMouse { set => prioritzaMouse = value; }

    //public InputActionReference SetInputActionReference(InputActionReference inputActionReference) => inputBinding = inputActionReference;
    public InputActionReference SetInputActionReference { set => inputBinding = value; }

    void OnEnable() 
    {
        if (inputBinding == null)
            return;

        MostrarIcone(inputBinding.action, overrided, prioritzaMouse, tipusForçat == null ? GetReconeixementTipus : tipusForçat);
    }



    public void MostrarIcone(bool forçar) 
    {
        if (forçar) trobat = false;
        MostrarIcone(inputBinding.action, overrided, prioritzaMouse, tipusForçat == null ? GetReconeixementTipus : tipusForçat);
    }
    public void MostrarIcone(InputActionReference inputActionReference, bool forçar = false) 
    {
        if (forçar) trobat = false;
        inputBinding = inputActionReference;
        MostrarIcone(inputActionReference.action, overrided, prioritzaMouse, tipusForçat == null ? GetReconeixementTipus : tipusForçat);
    } 

    [ContextMenu("Provar")]
    void Provar()
    {
        trobat = false;
        MostrarIcone(inputBinding.action, overrided, prioritzaMouse, tipusForçat == null ? GetReconeixementTipus : tipusForçat);
    }
}
