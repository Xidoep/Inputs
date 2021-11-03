using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using XS_Utils;

public abstract class Input_Icone : MonoBehaviour
{
    public GameObject binding;
    public GameObject fondo;
    [Space(10)]
    public GameObject etiqueta;
    public LocalizedString texte;
    public LocalizedAsset<TMP_FontAsset> fonts;
    public Input_Reconeixement reconeixement;

    bool trobat = false;



    public void MostrarIcone(InputAction accio)
    {
        if (trobat)
            return;

        trobat = true;
        InputUser.onChange += Resetejar;

        //XS_Utils.Inputs.Icone icone = XS_Utils.Inputs.GetIcone(reconeixement, accio);
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        Debug.Log(playerInput.gameObject.name);
        Debug.Log(playerInput.devices[0]);

        Inputs_Utils.Icone icone = reconeixement.GetIcone(accio, playerInput.devices[0]);

        //binding.GetComponent<SpriteRenderer>()?.sprite = icone.icone;
        //fondo.GetComponent<SpriteRenderer>()?.sprite = icone.fondo;
        binding.GetComponent<SpriteRenderer>()?.Sprite(icone.icone);
        if (fondo != null) fondo.GetComponent<SpriteRenderer>()?.Sprite(icone.fondo);
        binding.GetComponent<Image>()?.Sprite(icone.icone);
        if (fondo != null) fondo.GetComponent<Image>()?.Sprite(icone.fondo);
        texte.StringChanged += ActualitzarEtiqueta;
        fonts.AssetChanged += ActualitzarEtiqueta;
        //texte.GetTraduccio((UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<string> resultat) => { etiqueta.GetComponent<TMP_Text>()?.SetText(resultat.Result); });
    }

    private void OnDisable()
    {
        texte.StringChanged -= ActualitzarEtiqueta;
        fonts.AssetChanged -= ActualitzarEtiqueta;
    }

    void ActualitzarEtiqueta(string s)
    {
        etiqueta.GetComponent<TMP_Text>()?.SetText(s);
    }
    void ActualitzarEtiqueta(TMP_FontAsset s)
    {
        etiqueta.GetComponent<TMP_Text>().font = s;
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

public static class ExtensionsSprite
{
    public static void Sprite(this SpriteRenderer spriteRenderer, Sprite sprite) { if (spriteRenderer != null) spriteRenderer.sprite = sprite; }
    public static void Sprite(this Image image, Sprite sprite) { if (image != null) image.sprite = sprite; }
}


