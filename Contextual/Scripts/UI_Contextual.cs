using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Xido Studio/Menu/UI Contextual", fileName = "Contextual")]
public class UI_Contextual : ScriptableObject
{
    [System.Serializable]
    public struct Icone
    {
        public GameObject icone;
        public InputActionReference action;
    }

    [SerializeField] GameObject prefabContextual;
    [SerializeField] GameObject prefabBinding;
    Transform parent;

    List<Icone> icones;

    //public List<InputActionReference> inputActions;

    public void OnEnable()
    {
        parent = null;
        icones = new List<Icone>();
    }
    public void OnDisable()
    {
        parent = null;
        icones = new List<Icone>();
    }

    public void Show(InputActionReference action)
    {
        if (parent == null) 
        {
            parent = Instantiate(prefabContextual).transform.GetChild(0);
        }

        //if (inputActions == null) inputActions = new List<InputActionReference>();
        //inputActions.Add(inputAction);
        GameObject icone = Instantiate(prefabBinding, parent);
        icone.GetComponent<Input_IconePerBinding>()?.MostrarIcone(action);
        
        if (icones == null) icones = new List<Icone>();
        icones.Add(new Icone() 
        {
            icone = icone,
            action = action
        });
        
    }

    public void Hide(InputActionReference inputAction)
    {
        bool trobat = false;
        int index = 0;
        while(index < icones.Count && !trobat)
        {
            if (icones[index].action == inputAction) trobat = true;
        }

        if (!trobat) 
            return;

        Destroy(icones[index].icone);
        icones.RemoveAt(index);

        if (icones.Count != 0)
            return;

        Destroy(parent.GetComponentInParent<Canvas>().gameObject);

    }
}
