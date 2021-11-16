using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

public static class Inputs_Utils
{
    static bool iguals;
    static int index;
    public static bool GetBool(this InputActionReference actionReference) => actionReference.action.ReadValue<float>() > 0.1f;
    public static float GetFloat(this InputActionReference actionReference) => actionReference.action.ReadValue<float>();
    public static Vector2 GetVector2(this InputActionReference actionReference) => actionReference.action.ReadValue<Vector2>();

    const string KEY_2DVECTOR = "2DVector";

    [Serializable]
    public struct Icone
    {
        public Sprite icone;
        public Sprite fondo;
    }

    /// <summary>
    /// Compara el string "path" de tipus amb el nom del inputDevice i retona si coincideixen.
    /// </summary>
    /// <param name="tipus"></param>
    /// <param name="inputDevice"></param>
    /// <returns></returns>
    public static bool Comparar(this Input_ReconeixementTipus tipus, InputDevice inputDevice)
    {
        iguals = false;
        index = 0;
        while (index < tipus.paths.Length && !iguals)
        {
            if (inputDevice.name.StartsWith(tipus.paths[index])) iguals = true;
            else index++;
        }
        return iguals;
    }

    public static bool Comparar(this InputAction inputAction, string path)
    {
        iguals = false;
        index = 0;
        while (index < inputAction.bindings.Count && !iguals)
        {
            if (inputAction.bindings[index].path.Equals(path)) iguals = true;
            else index++;
        }
        return iguals;
    }

    public static bool Comparar(this InputBinding inputBinding, string path) => inputBinding.PathOrOverridePath() == path;
    /*{
        if (string.IsNullOrEmpty(inputBinding.overridePath))
            return inputBinding.path == path;
        else return inputBinding.overridePath == path;
    }*/

    /// <summary>
    /// Retorna el pathoverride si n'hi ha un, si no, retorna el path per defecte.
    /// </summary>
    /// <param name="inputBinding"></param>
    /// <returns></returns>
    public static string PathOrOverridePath(this InputBinding inputBinding)
    {
        if (string.IsNullOrEmpty(inputBinding.overridePath))
            return inputBinding.path;
        else return inputBinding.overridePath;
    }
    static Input_ReconeixementTipus input;

    public static bool EsComposada(this InputAction accio, InputDevice inputDevice)
    {
        bool composada = false;
        for (int b = 0; b < accio.bindings.Count; b++)
        {
            for (int a = 0; a < input.tecles.Length; a++)
            {
                composada = accio.bindings[b].PathOrOverridePath() == KEY_2DVECTOR;
                return composada;
            }
        }
        return composada;
    }

    public static Icone GetIcone(this Input_Reconeixement reconeixement, InputAction accio, InputDevice inputDevice)
    {
        Icone icone = new Icone() { icone = null, fondo = null };

        input = null;
        //Trobar tipus d'imput
        for (int r = 0; r < reconeixement.inputs.Count; r++)
        {
            for (int p = 0; p < reconeixement.inputs[r].paths.Length; p++)
            {
                Debugar.Log(reconeixement.inputs[r].paths[p]);
                if (inputDevice.name.StartsWith(reconeixement.inputs[r].paths[p]))
                {
                    Debugar.Log($"Trobat! {reconeixement.inputs[r].name}");
                    input = reconeixement.inputs[r];
                    break;
                }
            }
        }

        //Trobar el binding
        if (input == null)
            return icone;

        for (int b = 0; b < accio.bindings.Count; b++)
        {
            for (int a = 0; a < input.tecles.Length; a++)
            {
                //Debugar.Log($"(Comparar path){a}      (reconeixement) {reconeixement.inputs[r].tecles[a].Path} == (accio) {accio.bindings[b].path}???");

                //PROVAR 
                if (accio.bindings[b].PathOrOverridePath() == KEY_2DVECTOR)
                {
                    Debugar.Log($"¿¿¿{input.tecles[a].Path}???");
                    if (string.Equals(input.tecles[a].Path, accio.bindings[b + 1].PathOrOverridePath()))
                    {
                        return new Icone() { icone = input.tecles[a].sprite, fondo = input.tecles[a].fondo };
                    }
                    b += 4;
                }
                else
                {
                    Debugar.Log($"¿¿¿{input.tecles[a].Path}???");
                    if (string.Equals(input.tecles[a].Path, accio.bindings[b].PathOrOverridePath()))
                    {
                        return new Icone() { icone = input.tecles[a].sprite, fondo = input.tecles[a].fondo };
                    }

                }
                
            }
            for (int a = 0; a < input.ratoli.Length; a++)
            {
                Debugar.Log($"¿¿¿{input.ratoli[a].Path}???");
                if (string.Equals(input.ratoli[a].Path, accio.bindings[b].PathOrOverridePath()))
                {
                    return new Icone() { icone = input.ratoli[a].sprite, fondo = input.ratoli[a].fondo };
                }
            }
            for (int a = 0; a < input.botons.Length; a++)
            {
                Debugar.Log($"¿¿¿{input.botons[a].Path}???");
                if (string.Equals(input.botons[a].Path, accio.bindings[b].PathOrOverridePath()))
                {
                    return new Icone() { icone = input.botons[a].sprite, fondo = input.botons[a].fondo };
                }
            }
            for (int a = 0; a < input.tactil.Length; a++)
            {
                Debugar.Log($"¿¿¿{input.tactil[a].Path}???");
                if (string.Equals(input.tactil[a].Path, accio.bindings[b].PathOrOverridePath()))
                {
                    return new Icone() { icone = input.tactil[a].sprite, fondo = input.tactil[a].fondo };
                }
            }
        }

        return icone;
    }

    /// <summary>
    /// Retorna el device el jugador 1.
    /// </summary>
    public static InputDevice GetDevice => PlayerInput.GetPlayerByIndex(0).devices[0];

    //public static void AfegirPerfomrmedAction(this InputActionReference inputActionReference, Action<InputAction.CallbackContext> accio) => inputActionReference.action.ReadValue<float>().action.performed += accio;

}
