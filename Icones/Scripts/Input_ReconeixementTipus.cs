using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Inputs/Tipus", fileName = "Input_ReconeixementTipus")]
public class Input_ReconeixementTipus : ScriptableObject
{
    /*[ListToPopup(typeof(Input_Reconeixement), "Esquemes")]*/ //public string Esquema;
    public string[] paths;
    //public RuntimePlatform[] plataformes;

    public Binding[] bindings;

    public Sprite fondoComposat;
    public Sprite fondo1D;
    public Sprite separador;

    [System.Serializable]
    public class Binding
    {
        [SerializeField] string path;
        [SerializeField] Sprite sprite;
        [SerializeField] Sprite fondo;
        public string Path => path;
        public Sprite Sprite => sprite;
        public Sprite Fondo => fondo;

        public XS_Input.Icone Icone(bool overrided) => new XS_Input.Icone() {
            path = path, 
            overrided = overrided,
            icone = sprite, 
            fondo = fondo,
        };
    }

}
