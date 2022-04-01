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

    private void OnEnable()
    {
        Debugar.Log("[Input_Reconeixement] OnEnable => ComprovarPlataforma()");
        ComprovarPlataforma();
    }

    void TeclatRatoli() => actual = inputs[0];
    void GamepadGeneric() => actual = inputs[1];
    void Tactil() => actual = inputs[2];
    void PS() => actual = inputs[3];
    void Switch() => actual = inputs[4];
    void Xbox() => actual = inputs[5];

    public void ComprovarPlataforma()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.LinuxPlayer:
            case RuntimePlatform.LinuxEditor:
            case RuntimePlatform.WebGLPlayer:
            case RuntimePlatform.tvOS:
            case RuntimePlatform.Stadia:
            case RuntimePlatform.CloudRendering:
            case RuntimePlatform.EmbeddedLinuxArm64:
            case RuntimePlatform.EmbeddedLinuxArm32:
            case RuntimePlatform.EmbeddedLinuxX64:
            case RuntimePlatform.EmbeddedLinuxX86:
            case RuntimePlatform.LinuxServer:
            case RuntimePlatform.WindowsServer:
            case RuntimePlatform.OSXServer:
                TeclatRatoli();
                break;
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.Android:
            case RuntimePlatform.WSAPlayerX86:
            case RuntimePlatform.WSAPlayerX64:
            case RuntimePlatform.WSAPlayerARM:
            case RuntimePlatform.Lumin:
                Tactil();
                break;
            case RuntimePlatform.PS4:
            case RuntimePlatform.PS5:
                PS();
                break;
            case RuntimePlatform.XboxOne:
            case RuntimePlatform.GameCoreXboxOne:
                Xbox();
                break;
            case RuntimePlatform.Switch:
                Switch();
                break;
        }
    }

}

