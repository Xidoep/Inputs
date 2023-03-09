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

    protected void MostrarIcone(InputAction accio, bool overrided)
    {
        if (accio == null)
            return;

        if (trobat)
            return;

        trobat = true;
        InputUser.onChange += Resetejar;

        //XS_Utils.Inputs.Icone icone = XS_Utils.Inputs.GetIcone(reconeixement, accio);
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        //Debug.Log(playerInput.gameObject.name);
        //Debug.Log(playerInput.devices[0]);

        Input_ReconeixementTipus input = reconeixement.TipusInput(Application.isPlaying ? playerInput.devices[0] : null, overrided);

        FindRenderers();


        if (input == null)
            return;

        if (input.paths[0] == "Keyboard")
        {
            
            if (accio.Es2D(Application.isPlaying ? playerInput.devices[0] : null, overrided))
            {
                Debug.Log("2D");
                Icone2D(input, accio, overrided);
            }
            else if (accio.Es1D(Application.isPlaying ? playerInput.devices[0] : null, overrided))
            {
                Debug.Log("1D");
                Icone1D(input, accio, overrided);
            }
            else
            {
                Debug.Log("Simple");
                IconeSimple(input, accio, Application.isPlaying ? playerInput.devices[0] : null, overrided);
            }
        }
        else
        {
            Debug.Log("No Teclat");
            IconeSimple(input, accio, Application.isPlaying ? playerInput.devices[0] : null, overrided);
        }

    }

    void IconeSimple(Input_ReconeixementTipus input, InputAction accio, InputDevice inputDevice, bool overrided)
    {
        if(bindingsComposats != null)
        {
            for (int i = 0; i < bindingsComposats.Count; i++)
            {
                Destroy(bindingsComposats[i]);
            }
        }
       

        XS_Input.Icone icone = input.GetIcone(accio, inputDevice, overrided);

        SetEnableBinding = true;
        SetSpriteBinding = icone.icone;
        SetSpriteFondo = icone.fondo;
        SetSizeFondo = Vector3.one;
    }
    void Icone2D(Input_ReconeixementTipus input, InputAction accio, bool overrided)
    {
        if (bindingsComposats == null) bindingsComposats = new List<GameObject>();

        //Neteja
        SetSpriteBinding = null;
        SetEnableBinding = false;
        //Busca icones
        XS_Input.Icone[] icones = input.GetIcone2D(accio, overrided);

        //Crea una imatge per cada icone
        for (int i = 0; i < icones.Length; i++)
        {
            GameObject iconeComposada = new GameObject();
            bindingsComposats.Add(iconeComposada);

            //posicionar
            iconeComposada.transform.SetParent(transform);
            iconeComposada.transform.localPosition = iconeComposada.transform.Posicio2D_PerIndex(i);
            iconeComposada.transform.localScale = Vector3.one * 0.2f;

            Image image = iconeComposada.AddComponent<Image>();
            image.sprite = icones[i].icone;
            image.color = GetColorBinding;

        }
        SetSpriteFondo = input.fondoComposat;
        //SetSizeFondo = Vector3.one * 1.4f;
        SetSizeFondo = Vector3.one;
    }

    void Icone1D(Input_ReconeixementTipus input, InputAction accio, bool overrided)
    {

        if (bindingsComposats == null) bindingsComposats = new List<GameObject>();

        //Neteja
        SetSpriteBinding = null;
        SetEnableBinding = false;
        //Busca icones
        XS_Input.Icone[] icones = input.GetIcone1D(accio, overrided);

        //Crea una imatge per cada icone
        for (int i = 0; i < icones.Length; i++)
        {
            GameObject iconeComposada = new GameObject();
            bindingsComposats.Add(iconeComposada);

            //posicionar
            iconeComposada.transform.SetParent(transform);
            iconeComposada.transform.localPosition = iconeComposada.transform.Posicio1D_PerIndex(i);
            iconeComposada.transform.localScale = Vector3.one * 0.23f;

            Image image = iconeComposada.AddComponent<Image>();
            image.sprite = icones[i].icone;
            image.color = GetColorBinding;

        }
        GameObject separador = new GameObject();
        bindingsComposats.Add(separador);

        separador.transform.SetParent(transform);
        separador.transform.localPosition = separador.transform.Posicio1D_PerIndex(2);
        separador.transform.localScale = Vector3.one * 0.3f;

        Image imgSep = separador.AddComponent<Image>();
        imgSep.sprite = input.separador;
        imgSep.color = GetColorBinding;

        SetSpriteFondo = input.fondo1D;
        //SetSizeFondo = Vector3.one * 1.4f;
        SetSizeFondo = Vector3.one;
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
