﻿using System.Collections.Generic;
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
    [SerializeField] TipoBinding tipoBinding;
    [SerializeField] List<XS_Input.Icone> icones;
    [SerializeField] GameObject binding;
    [SerializeField] GameObject fondo;
    List<GameObject> bindingsComposats;

    internal bool trobat = false;

    Image bindingImage;
    SpriteRenderer bindingSpriteRenderer;

    Image fondoImage;
    SpriteRenderer fondoSpriteRenderer;

    GameObject icone;
    Image imgSep;

    Sprite BindingSprite
    {
        set
        {
            if (bindingImage != null) bindingImage.sprite = value;
            else if (bindingSpriteRenderer != null) bindingSpriteRenderer.sprite = value;
        }
    }
    bool BindingEnable
    {
        set
        {
            if (bindingImage != null) bindingImage.enabled = value;
            else if (bindingSpriteRenderer != null) bindingSpriteRenderer.enabled = value;
        }
    }
    Color BindingColor
    {
        get
        {
            if (bindingImage != null) return bindingImage.color;
            else if (bindingSpriteRenderer != null) return bindingSpriteRenderer.color;
            else return Color.black;
        }
    }
    Sprite FondoSprite
    {
        set
        {
            if (!fondo)
                return;

            if (fondoImage != null) fondoImage.sprite = value;
            else if (fondoSpriteRenderer != null) fondoSpriteRenderer.sprite = value;
        }
    }
    Vector3 FondoSize
    {
        set
        {
            if (fondoImage != null) fondoImage.transform.localScale = value;
            else if (fondoSpriteRenderer != null) fondoSpriteRenderer.transform.localScale = value;
        }
    }

    public List<XS_Input.Icone> Icones => icones;



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

    protected void MostrarIcone(InputAction accio, bool overrided, bool prioritzaMouse)
    {
        if (accio == null)
            return;

        if (trobat)
            return;

        trobat = true;
        InputUser.onChange += Resetejar;

        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        Input_ReconeixementTipus input = reconeixement.TipusInput(Application.isPlaying ? playerInput.devices[0] : null, overrided);

        FindRenderers();

        Debug.Log(input.paths[0]);

        icones = new List<XS_Input.Icone>();

        tipoBinding = TipoBinding.Simple;
        for (int ab = 0; ab < accio.bindings.Count; ab++)
        {
            //Debug.Log(accio.bindings[ab].path);
            for (int ib = 0; ib < input.bindings.Length; ib++)
            {

                if (string.Equals(input.bindings[ib].Path, accio.bindings[ab].PathOrOverridePath(overrided)))
                {
                    if(input.paths[0] == "Keyboard")
                    {
                        if (prioritzaMouse)
                        {
                            if (accio.bindings[ab].PathOrOverridePath(overrided).Contains("Keyboard"))
                                continue;
                        }
                        else
                        {
                            if (accio.bindings[ab].PathOrOverridePath(overrided).Contains("Mouse"))
                                continue;
                        }
                    }
                    //Debug.Log(accio.bindings[ab].name);



                    //Debug.Log($" **************************************{input.bindings[ib].Path}");

                    if(ab > 0)
                    {
                        if (accio.bindings[ab - 1].PathOrOverridePath(overrided) == "OneModifier") tipoBinding = TipoBinding.OnModifier;
                        if (accio.bindings[ab - 1].PathOrOverridePath(overrided) == "1DAxis") tipoBinding = TipoBinding.Axis;
                        if (accio.bindings[ab - 1].PathOrOverridePath(overrided) == "2DVector") tipoBinding = TipoBinding.Vector2;
                    }

                    icones.Add(input.bindings[ib].Icone(accio.bindings[ab].IsOverrdided()));
                }
            }
        }

        if(tipoBinding != TipoBinding.Simple && bindingsComposats != null)
        {
            for (int i = 0; i < bindingsComposats.Count; i++)
            {
                Destroy(bindingsComposats[i]);
            }
            bindingsComposats.Clear();
        }

        switch (tipoBinding)
        {
            case TipoBinding.Simple:
                BindingEnable = true;
                BindingSprite = icones[0].icone;

                FondoSprite = icones[0].fondo;
                break;
            case TipoBinding.OnModifier:
            case TipoBinding.Axis:
                BindingEnable = false;
                BindingSprite = null;

                FondoSprite = input.fondo1D;

                for (int i = 0; i < 2; i++)
                {
                    icone = new GameObject();
                    if (bindingsComposats == null) bindingsComposats = new List<GameObject>();
                    bindingsComposats.Add(icone);

                    //posicionar
                    icone.transform.SetParent(transform);
                    icone.transform.localPosition = icone.transform.Posicio1D_PerIndex(i);
                    icone.transform.localRotation = Quaternion.identity;
                    icone.transform.localScale = Vector3.one * 0.23f;

                    Image image = icone.AddComponent<Image>();
                    image.sprite = icones[i].icone;
                    image.color = BindingColor;
                }
                break;
            case TipoBinding.Vector2:
                BindingEnable = false;
                BindingSprite = null;

                FondoSprite = input.fondoComposat;

                for (int i = 0; i < 4; i++)
                {
                    icone = new GameObject();
                                        if (bindingsComposats == null) bindingsComposats = new List<GameObject>();
                    bindingsComposats.Add(icone);

                    //posicionar
                    icone.transform.SetParent(transform);
                    icone.transform.localPosition = icone.transform.Posicio2D_PerIndex(i);
                    icone.transform.localRotation = Quaternion.identity;
                    icone.transform.localScale = Vector3.one * 0.2f;

                    Image image = icone.AddComponent<Image>();
                    image.sprite = icones[i].icone;
                    image.color = BindingColor;
                }
                break;
        }

        //Extra detalls
        switch (tipoBinding)
        {
            case TipoBinding.Simple:
                break;
            case TipoBinding.OnModifier:
                GameObject separador = new GameObject();
                bindingsComposats.Add(separador);

                separador.transform.SetParent(transform);
                separador.transform.localPosition = separador.transform.Posicio1D_PerIndex(2);
                separador.transform.localScale = Vector3.one * 0.3f;

                imgSep = separador.AddComponent<Image>();
                imgSep.sprite = input.separador;
                imgSep.color = BindingColor;
                break;
            case TipoBinding.Axis:
                GameObject mes = new GameObject();
                bindingsComposats.Add(mes);

                mes.transform.SetParent(transform);
                mes.transform.localPosition = mes.transform.Posicio1D_PerIndex(2);
                mes.transform.localScale = Vector3.one * 0.3f;

                imgSep = mes.AddComponent<Image>();
                imgSep.sprite = input.separador;
                imgSep.color = BindingColor;
                break;
            case TipoBinding.Vector2:
                break;
            default:
                break;
        }
/*
        return;


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
            else if(accio.EsOneModifier(Application.isPlaying ? playerInput.devices[0] : null, overrided))
            {
                Debug.Log("One Modifier");
                IconeOnModifier(input, accio, overrided);
            }
            else
            {
                Debug.Log("Simple");
                Icone(input, accio, Application.isPlaying ? playerInput.devices[0] : null, overrided);
            }
        }
        else
        {
            Debug.Log("No Teclat");
            Icone(input, accio, Application.isPlaying ? playerInput.devices[0] : null, overrided);
        }
*/
    }

    /*
    void Icone(Input_ReconeixementTipus input, InputAction accio, InputDevice inputDevice, bool overrided)
    {
        if(bindingsComposats != null)
        {
            for (int i = 0; i < bindingsComposats.Count; i++)
            {
                Destroy(bindingsComposats[i]);
            }
        }

       

        XS_Input.Icone icone = input.GetIcone(accio, inputDevice, overrided);

        BindingEnable = true;
        BindingSprite = icone.icone;
        FondoSprite = icone.fondo;
        FondoSize = Vector3.one;
    }
    void Icone2D(Input_ReconeixementTipus input, InputAction accio, bool overrided)
    {
        if (bindingsComposats == null) bindingsComposats = new List<GameObject>();

        //Neteja
        BindingSprite = null;
        BindingEnable = false;
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
            image.color = BindingColor;

        }
        FondoSprite = input.fondoComposat;
        //SetSizeFondo = Vector3.one * 1.4f;
        FondoSize = Vector3.one;
    }

    void Icone1D(Input_ReconeixementTipus input, InputAction accio, bool overrided)
    {

        if (bindingsComposats == null) bindingsComposats = new List<GameObject>();

        //Neteja
        BindingSprite = null;
        BindingEnable = false;
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
            image.color = BindingColor;

        }
        GameObject separador = new GameObject();
        bindingsComposats.Add(separador);

        separador.transform.SetParent(transform);
        separador.transform.localPosition = separador.transform.Posicio1D_PerIndex(2);
        separador.transform.localScale = Vector3.one * 0.3f;

        Image imgSep = separador.AddComponent<Image>();
        imgSep.sprite = input.separador;
        imgSep.color = BindingColor;

        FondoSprite = input.fondo1D;
        //SetSizeFondo = Vector3.one * 1.4f;
        FondoSize = Vector3.one;
    }
    void IconeOnModifier(Input_ReconeixementTipus input, InputAction accio, bool overrided)
    {

        if (bindingsComposats == null) bindingsComposats = new List<GameObject>();

        //Neteja
        BindingSprite = null;
        BindingEnable = false;
        //Busca icones
        XS_Input.Icone[] icones = input.GetIconeOnModifier(accio, overrided);

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
            image.color = BindingColor;

        }
        GameObject separador = new GameObject();
        bindingsComposats.Add(separador);

        separador.transform.SetParent(transform);
        separador.transform.localPosition = separador.transform.Posicio1D_PerIndex(2);
        separador.transform.localScale = Vector3.one * 0.3f;

        Image imgSep = separador.AddComponent<Image>();
        imgSep.sprite = input.separador;
        imgSep.color = BindingColor;

        FondoSprite = input.fondo1D;
        //SetSizeFondo = Vector3.one * 1.4f;
        FondoSize = Vector3.one;
    }
    */

    void Resetejar(InputUser inputUser, InputUserChange inputUserChange, InputDevice inputDevice)
    {
        if (inputUserChange == InputUserChange.DevicePaired)
        {
            trobat = false;
            InputUser.onChange -= Resetejar;
        }
    }


    public enum TipoBinding
    {
        Simple, OnModifier, Axis, Vector2
    }
}
