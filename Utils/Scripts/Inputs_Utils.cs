using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

public static class Inputs_Utils
{
    static bool debug = true;

    static bool iguals;
    static int index;
    //public static bool GetBool(this InputActionReference actionReference) => actionReference.action.ReadValue<float>() > 0.1f;
    //public static float GetFloat(this InputActionReference actionReference) => actionReference.action.ReadValue<float>();
    //public static bool IsZero(this InputActionReference actionReference) => actionReference.action.ReadValue<Vector2>() != Vector2.zero;
    //public static Vector2 GetVector2(this InputActionReference actionReference) => actionReference.action.ReadValue<Vector2>();

    //const string KEY_2DVECTOR = "2DVector";
    //const string KEY_1DVECTOR = "1DAxis";

    /*[Serializable]
    public struct Icone
    {
        public Sprite icone;
        public Sprite fondo;
    }*/

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

    //public static bool Comparar(this InputBinding inputBinding, string path, bool overrided) => inputBinding.PathOrOverridePath() == path;
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
    /*public static string PathOrOverridePath(this InputBinding inputBinding, bool overrided)
    {
        if (overrided)
        {
            if (string.IsNullOrEmpty(inputBinding.overridePath))
                return inputBinding.path;
            else return inputBinding.overridePath;
        }
        else return inputBinding.path;
    }*/
    //static Input_ReconeixementTipus input;

    public static Input_ReconeixementTipus TipusInput(this Input_Reconeixement reconeixement, InputDevice inputDevice = null)
    {
        Input_ReconeixementTipus input = null;
        for (int r = 0; r < reconeixement.inputs.Count; r++)
        {
            for (int p = 0; p < reconeixement.inputs[r].paths.Length; p++)
            {
                if (inputDevice != null)
                {
                    if (inputDevice.name.StartsWith(reconeixement.inputs[r].paths[p]))
                    {
                        if (debug) Debugar.Log($"{reconeixement.inputs[r].name}");
                        input = reconeixement.inputs[r];
                        break;
                    }
                }
                else
                {
                    //if (inputDevice.name.StartsWith(reconeixement.inputs[r].paths[p]))
                    //{
                        if (debug) Debugar.Log($"{reconeixement.inputs[r].paths[p]}");
                        input = reconeixement.inputs[r];
                        break;
                    //}
                }
            }
            if (input != null)
            {
                Debug.LogError($"input = {input.name}");
                break;
            }
        }
        return input;
    }

    public static XS_Input.Icone[] GetIcone1D(this Input_ReconeixementTipus input, InputAction accio) => input.GetIcone2D(accio, false); 


    public static XS_Input.Icone[] GetIcone2D(this Input_ReconeixementTipus input, InputAction accio, bool overrided, bool es2D = true)
    {
        List<XS_Input.Icone> icones = new List<XS_Input.Icone>();
        for (int b = 0; b < accio.bindings.Count; b++)
        {
            if (b > 4)
                break;

            if (accio.bindings[b].PathOrOverridePath(overrided) == (es2D ? XS_Input.KEY_2DVECTOR : XS_Input.KEY_1DVECTOR))
            {
                for (int i = 1; i < (es2D ? 5 : 3); i++)
                {
                    for (int a = 0; a < input.tecles.Length; a++)
                    {
                        if (debug) Debugar.Log($"¿¿¿{input.tecles[a].Path}???");
                        if (string.Equals(input.tecles[a].Path, accio.bindings[b + i].PathOrOverridePath(overrided)))
                        {
                            if (debug) Debugar.Log($"¡¡¡¡¡{input.tecles[a].Path}!!!!!");
                            icones.Add(
                                new XS_Input.Icone()
                                {
                                    icone = input.tecles[a].sprite,
                                    fondo = input.tecles[a].fondo
                                });
                            break;
                        }
                        
                    }
                }
                
            }
        }
        return icones.ToArray();
    }


    public static XS_Input.Icone GetIcone(this Input_ReconeixementTipus input, InputAction accio, InputDevice inputDevice, bool overrided = true)
    {
        XS_Input.Icone icone = new XS_Input.Icone() { icone = null, fondo = null };

        //Input_ReconeixementTipus input = null;
        //Input_ReconeixementTipus input = reconeixement.TipusInput(inputDevice);

        //Trobar tipus d'imput
        /*for (int r = 0; r < reconeixement.inputs.Count; r++)
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
        }*/

        //Trobar el binding
        if (input == null)
            return icone;

        for (int b = 0; b < accio.bindings.Count; b++)
        {
            for (int a = 0; a < input.tecles.Length; a++)
            {
                //Debugar.Log($"(Comparar path){a}      (reconeixement) {reconeixement.inputs[r].tecles[a].Path} == (accio) {accio.bindings[b].path}???");

                //PROVAR 
                if (accio.bindings[b].PathOrOverridePath(overrided) == XS_Input.KEY_2DVECTOR)
                {
                    if (debug) Debugar.Log($"¿¿¿{input.tecles[a].Path}???");
                    if (string.Equals(input.tecles[a].Path, accio.bindings[b + 1].PathOrOverridePath(overrided)))
                    {
                        if (debug) Debugar.Log($"¡¡¡¡¡{input.tecles[a].Path}!!!!!");
                        return new XS_Input.Icone() { icone = input.tecles[a].sprite, fondo = input.tecles[a].fondo };
                    }
                    b += 4;
                }
                else
                {
                    if (debug) Debugar.Log($"¿¿¿{input.tecles[a].Path}???");
                    if (string.Equals(input.tecles[a].Path, accio.bindings[b].PathOrOverridePath(overrided)))
                    {
                        if (debug) Debugar.Log($"¡¡¡¡¡{input.tecles[a].Path}!!!!!");
                        return new XS_Input.Icone() { icone = input.tecles[a].sprite, fondo = input.tecles[a].fondo };
                    }
                }
                
            }
            for (int a = 0; a < input.ratoli.Length; a++)
            {
                if (debug) Debugar.Log($"¿¿¿{input.ratoli[a].Path}???");
                if (string.Equals(input.ratoli[a].Path, accio.bindings[b].PathOrOverridePath(overrided)))
                {
                    return new XS_Input.Icone() { icone = input.ratoli[a].sprite, fondo = input.ratoli[a].fondo };
                }
            }
            for (int a = 0; a < input.botons.Length; a++)
            {
                if (debug) Debugar.Log($"¿¿¿{input.botons[a].Path}???");
                if (string.Equals(input.botons[a].Path, accio.bindings[b].PathOrOverridePath(overrided)))
                {
                    return new XS_Input.Icone() { icone = input.botons[a].sprite, fondo = input.botons[a].fondo };
                }
            }
            for (int a = 0; a < input.tactil.Length; a++)
            {
                if (debug) Debugar.Log($"¿¿¿{input.tactil[a].Path}???");
                if (string.Equals(input.tactil[a].Path, accio.bindings[b].PathOrOverridePath(overrided)))
                {
                    return new XS_Input.Icone() { icone = input.tactil[a].sprite, fondo = input.tactil[a].fondo };
                }
            }
            for (int a = 0; a < input.custom.Length; a++)
            {
                if (debug) Debugar.Log($"¿¿¿{input.custom[a].Path}???");
                if (string.Equals(input.custom[a].Path, accio.bindings[b].PathOrOverridePath(overrided)))
                {
                    return new XS_Input.Icone() { icone = input.custom[a].sprite, fondo = input.custom[a].fondo };
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
