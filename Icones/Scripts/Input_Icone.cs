using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using XS_Utils;

public abstract class Input_Icone : MonoBehaviour
{
    [SerializeField] Input_Reconeixement reconeixement;
    [SerializeField] GameObject binding;
    [SerializeField] GameObject fondo;
    List<GameObject> bindingsComposats;

    internal bool trobat = false;

    Image bindingImage;
    SpriteRenderer bindingSpriteRenderer;
    Image fondoImage;
    SpriteRenderer fondoSpriteRenderer;

    

    Sprite SetSpriteBinding
    {
        set
        {
            if (bindingImage != null) bindingImage.sprite = value;
            else if (bindingSpriteRenderer != null) bindingSpriteRenderer.sprite = value;
        }
    }
    bool SetEnableBinding
    {
        set
        {
            if (bindingImage != null) bindingImage.enabled = value;
            else if (bindingSpriteRenderer != null) bindingSpriteRenderer.enabled = value;
        }
    }
    Sprite SetSpriteFondo
    {
        set
        {
            if (!fondo)
                return;

            if (fondoImage != null) fondoImage.sprite = value;
            else if (fondoSpriteRenderer != null) fondoSpriteRenderer.sprite = value;
        }
    }
    Vector3 SetSizeFondo
    {
        set
        {
            if (fondoImage != null) fondoImage.transform.localScale = value;
            else if (fondoSpriteRenderer != null) fondoSpriteRenderer.transform.localScale = value;
        }
    }
    Color GetColorBinding
    {
        get
        {
            if (bindingImage != null) return bindingImage.color;
            else if (bindingSpriteRenderer != null) return bindingSpriteRenderer.color;
            else return Color.black;
        }
    }

    void FindRenderers()
    {
        if (bindingImage == null) bindingImage = binding.GetComponent<Image>();
        if (bindingImage == null) 
        {
            if (bindingSpriteRenderer == null) bindingSpriteRenderer = binding.GetComponent<SpriteRenderer>();
        }

        if (!fondo)
            return;

        if (fondoImage == null) fondoImage = fondo.GetComponent<Image>();
        if (fondoImage == null) 
        {
            if (fondoSpriteRenderer == null) fondoSpriteRenderer = fondo.GetComponent<SpriteRenderer>();
        }
    }

    protected void MostrarIcone(InputAction accio)
    {
        if (accio == null)
            return;

        if (trobat)
            return;

        trobat = true;
        InputUser.onChange += Resetejar;

        //XS_Utils.Inputs.Icone icone = XS_Utils.Inputs.GetIcone(reconeixement, accio);
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        Debug.Log(playerInput.gameObject.name);
        Debug.Log(playerInput.devices[0]);

        Input_ReconeixementTipus input = reconeixement.TipusInput(playerInput.devices[0]);

        FindRenderers();


        if (input == null)
            return;

        if (input.paths[0] == "Keyboard")
        {
            
            if (accio.EsComposada(playerInput.devices[0]))
            {
                Debug.Log("Composada");
                IconeComposte(input, accio);
            }
            else
            {
                Debug.Log("Simple");
                IconeSimple(input, accio, playerInput.devices[0]);
            }
        }
        else
        {
            Debug.Log("No Teclat");
            IconeSimple(input, accio, playerInput.devices[0]);
        }

    }

    void IconeSimple(Input_ReconeixementTipus input, InputAction accio, InputDevice inputDevice)
    {
        if(bindingsComposats != null)
        {
            for (int i = 0; i < bindingsComposats.Count; i++)
            {
                Destroy(bindingsComposats[i]);
            }
        }
       

        Inputs_Utils.Icone icone = input.GetIcone(accio, inputDevice);

        SetEnableBinding = true;
        SetSpriteBinding = icone.icone;
        SetSpriteFondo = icone.fondo;
        SetSizeFondo = Vector3.one;
    }
    void IconeComposte(Input_ReconeixementTipus input, InputAction accio)
    {
        if (bindingsComposats == null) bindingsComposats = new List<GameObject>();

        //Neteja
        SetSpriteBinding = null;
        SetEnableBinding = false;
        //Busca icones
        Inputs_Utils.Icone[] icones = input.GetIconeComposte(accio);

        //Crea una imatge per cada icone
        for (int i = 0; i < icones.Length; i++)
        {
            GameObject iconeComposada = new GameObject();
            bindingsComposats.Add(iconeComposada);

            //posicionar
            iconeComposada.transform.SetParent(transform);
            iconeComposada.transform.localPosition = PosicioPerIndex(i);
            iconeComposada.transform.localScale = Vector3.one * 0.2f;

            Image image = iconeComposada.AddComponent<Image>();
            image.sprite = icones[i].icone;
            image.color = GetColorBinding;

        }
        SetSpriteFondo = input.fondoComposat;
        //SetSizeFondo = Vector3.one * 1.4f;
        SetSizeFondo = Vector3.one;

    }

    Vector3 PosicioPerIndex(int i)
    {
        switch (i)
        {
            case 0:
                return transform.up * 9.7f;
            case 1:
                return transform.up * -8.3f;
            case 2:
                return transform.up * -8.3f + transform.right * -18;
            case 3:
                return transform.up * -8.3f + transform.right * 18;
            default:
                return Vector3.zero;
        }
    }

    void Resetejar(InputUser inputUser, InputUserChange inputUserChange, InputDevice inputDevice)
    {
        if (inputUserChange == InputUserChange.DevicePaired)
        {
            trobat = false;
            InputUser.onChange -= Resetejar;
        }
    }
}
