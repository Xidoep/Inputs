using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

public static class Inputs_Utils
{
    static bool debug = false;

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


    public static Input_ReconeixementTipus TipusInput(this Input_Reconeixement reconeixement, InputDevice inputDevice, bool overrided)
    {
        if (inputDevice == null) //DEBUG
        {
            return reconeixement.actual;
        }

        Input_ReconeixementTipus input = null;
        for (int r = 0; r < reconeixement.inputs.Count; r++)
        {
            for (int p = 0; p < reconeixement.inputs[r].paths.Length; p++)
            {
                if (inputDevice != null)
                {
                    if (inputDevice.name.StartsWith(reconeixement.inputs[r].paths[p]))
                    {
                       Debugar.Log($"{reconeixement.inputs[r].name}");
                        input = reconeixement.inputs[r];
                        break;
                    }
                }
                else
                {
                    //if (inputDevice.name.StartsWith(reconeixement.inputs[r].paths[p]))
                    //{
                        Debugar.Log($"{reconeixement.inputs[r].paths[p]}");
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

    /*
    public static XS_Input.Icone[] GetIconeOnModifier(this Input_ReconeixementTipus input, InputAction accio, bool overrided) => input.GetIcone2D(accio, overrided, 0); 
    public static XS_Input.Icone[] GetIcone1D(this Input_ReconeixementTipus input, InputAction accio, bool overrided) => input.GetIcone2D(accio, overrided, 1);
    public static XS_Input.Icone[] GetIcone2D(this Input_ReconeixementTipus input, InputAction accio, bool overrided, int Ds = 2)
    {
        List<XS_Input.Icone> icones = new List<XS_Input.Icone>();
        for (int b = 0; b < accio.bindings.Count; b++)
        {
            if (b > 4)
                break;

            switch (Ds)
            {
                case 0: 
                    Agafar(b, 3, XS_Input.KEY_ONEMODIFIER);
                    break;
                case 1:
                    Agafar(b, 3, XS_Input.KEY_1DVECTOR);
                    break;
                case 2:
                    Agafar(b, 5, XS_Input.KEY_2DVECTOR);
                    break;
            }

        }
        return icones.ToArray();

        void Agafar(int index, int capAball, string KEY)
        {
            if (accio.bindings[index].PathOrOverridePath(overrided) == KEY)
            {
                for (int i = 1; i < capAball; i++)
                {
                    for (int a = 0; a < input.tecles.Length; a++)
                    {
                        if (debug) Debugar.Log($"¿¿¿{input.tecles[a].Path}???");
                        if (string.Equals(input.tecles[a].Path, accio.bindings[index + i].PathOrOverridePath(overrided)))
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

    }

    public static XS_Input.Icone GetIcone(this Input_ReconeixementTipus input, InputAction accio, InputDevice inputDevice, bool overrided )
    {
        XS_Input.Icone icone = new XS_Input.Icone() { icone = null, fondo = null };




            //Trobar el binding
            if (input == null)
            return icone;

        for (int b = 0; b < accio.bindings.Count; b++)
        {
            for (int a = 0; a < input.tecles.Length; a++)
            {
                if (debug) Debugar.Log($"¿¿¿{input.tecles[a].Path}???");
                if (string.Equals(input.tecles[a].Path, accio.bindings[b].PathOrOverridePath(overrided)))
                {
                    if (debug) Debugar.Log($"¡¡¡¡¡{input.tecles[a].Path}!!!!!");
                    return new XS_Input.Icone() { icone = input.tecles[a].sprite, fondo = input.tecles[a].fondo };
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
    */

    /// <summary>
    /// Retorna el device el jugador 1.
    /// </summary>
    public static InputDevice GetDevice => PlayerInput.GetPlayerByIndex(0).devices[0];

    //public static void AfegirPerfomrmedAction(this InputActionReference inputActionReference, Action<InputAction.CallbackContext> accio) => inputActionReference.action.ReadValue<float>().action.performed += accio;

}
