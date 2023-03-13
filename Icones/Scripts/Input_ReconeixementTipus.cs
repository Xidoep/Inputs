﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

[CreateAssetMenu(menuName = "Xido Studio/Inputs/Tipus", fileName = "Input_ReconeixementTipus")]
public class Input_ReconeixementTipus : ScriptableObject
{
    /*[ListToPopup(typeof(Input_Reconeixement), "Esquemes")]*/ //public string Esquema;
    public string[] paths;
    //public RuntimePlatform[] plataformes;

    public Binding[] bindings;
    public Boto[] botons;
    public Tecla[] tecles;
    public Ratoli[] ratoli;
    public Tactil[] tactil;
    public Custom[] custom;

    public Sprite fondoComposat;
    public Sprite fondo1D;
    public Sprite separador;

    [System.Serializable]
    public class Binding
    {
        public string path;
        public Sprite sprite;
        public Sprite fondo;
        public virtual string Path => path;
    }

    [System.Serializable]
    public class Boto : Binding
    {
        public GamepadButton boto;
        public override string Path => $"<Gamepad>/{path}";
    }
    [System.Serializable]
    public class Tecla : Binding
    {
        public bool WASD_Fletxes;
        public Key key;
        public override string Path => $"<Keyboard>/{path}";
    }
    [System.Serializable]
    public class Ratoli : Binding
    {
        public MouseButton mouseButton;
        public Input_MouseInteraccions mouseInteraccio;
        public override string Path => $"<Mouse>/{path}";
    }
    [System.Serializable]
    public class Tactil : Binding
    {
        public Input_TactilInteraccions tactil;
        public override string Path => $"<Touchscreen>/{path}";
    }
    [System.Serializable]
    public class Custom : Binding
    {
        public override string Path => path;
    }
}
public enum Input_TactilInteraccions
{
    clicar, posicio, delta
}

public enum Input_MouseInteraccions
{
    cap, position, delta
}