using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using XS_Utils;

/// <summary>
/// It fires an event when the actual device changes. Super usefull for UI, and hide/show things on diferent input modes.
/// </summary>
public class Input_EnCanviarControls : MonoBehaviour
{
    [System.Serializable]
    public struct Comprovacions
    {
        public Input_ReconeixementTipus[] buscats;
        public UnityEvent EnCoincidir;
        public UnityEvent SiNo;
    }

    [SerializeField] bool enEnable;
    [SerializeField] Comprovacions[] comprovacions;

    bool trobat = false;
    int index = 0;
    

    void OnEnable()
    {
        if (enEnable) Comprovar();
        InputUser.onChange += EnCanviar;
    }

    void OnDisable()
    {
        InputUser.onChange -= EnCanviar;
    }



    void EnCanviar(InputUser inputUser, InputUserChange inputUserChange, InputDevice inputDevice)
    {
        if(inputUserChange == InputUserChange.DevicePaired)
        {
            //FUNCIONA!!!
            //Debug.Log("Iconne = " + reconeixement.AgafarIcone(inputActionReference.action, inputDevice).icone.name);

            Comprovar();
        }
    }

    void Comprovar()
    {
        for (int i = 0; i < comprovacions.Length; i++)
        {
            trobat = false;
            index = 0;
            while(index < comprovacions[i].buscats.Length && !trobat)
            {
                if (comprovacions[i].buscats[index].Comparar(PlayerInput.GetPlayerByIndex(0).devices[0])) 
                    trobat = true;
                else index ++;
            }

            if(trobat)
                comprovacions[i].EnCoincidir?.Invoke();
            else comprovacions[i].SiNo?.Invoke();
        }
    }







    [ContextMenu("Debug coincidir")]
    void DebugCoincidir()
    {
        for (int i = 0; i < comprovacions.Length; i++)
        {
            comprovacions[i].EnCoincidir?.Invoke();
        }
    }
    [ContextMenu("Debug NO coincidir")]
    void DebugNoCoincidir()
    {
        for (int i = 0; i < comprovacions.Length; i++)
        {
            comprovacions[i].SiNo?.Invoke();
        }
    }
}
