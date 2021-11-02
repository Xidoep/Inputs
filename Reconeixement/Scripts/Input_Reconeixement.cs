using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Switch;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Inputs/Reconeixement", fileName = "Input_Reconeixement")]
public class Input_Reconeixement : ScriptableObject
{
    public Input_ReconeixementTipus actual;
    public List<Input_ReconeixementTipus> inputs;

    public void ComprovarPlataforma()
    {
        Debugar.Log("Input x plataforma");
        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
                actual = inputs[6];//iOS
                break;
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                actual = inputs[0];//TeclatRatoli
                break;
            case RuntimePlatform.IPhonePlayer:
                actual = inputs[6];//iOS
                break;
            case RuntimePlatform.Android:
                actual = inputs[2];//Tàctil
                break;
            case RuntimePlatform.LinuxPlayer:
            case RuntimePlatform.LinuxEditor:
            case RuntimePlatform.WebGLPlayer:
                actual = inputs[0];//TeclatRatoli
                break;
            case RuntimePlatform.PS4:
            case RuntimePlatform.PS5:
                actual = inputs[3];//PS4
                break;
            case RuntimePlatform.XboxOne:
            case RuntimePlatform.GameCoreXboxOne:
            case RuntimePlatform.GameCoreXboxSeries:
                actual = inputs[5];//Xbox
                break;
            case RuntimePlatform.tvOS:
                actual = inputs[6];//iOS
                break;
            case RuntimePlatform.Switch:
                actual = inputs[4];//Switch
                break;
            case RuntimePlatform.Lumin: //linux
            case RuntimePlatform.Stadia: //Posar una UI que es pugui clicar amb el dit o amb el ratolí per tornarse GAMEPAD.
            case RuntimePlatform.CloudRendering: //Posar una UI que es pugui clicar amb el dit o amb el ratolí per tornarse GAMEPAD.
                actual = inputs[0];//TeclatRatoli [Possible amb tots els tipus]
                break;
            default:
                actual = inputs[0];//TeclatRatoli
                break;
        }
    }

}

