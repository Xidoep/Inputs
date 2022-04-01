using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XS_Utils;

[CreateAssetMenu(menuName = "Xido Studio/Menu/UI Contextual", fileName = "Contextual")]
public class UI_Contextual : ScriptableObject
{
    [System.Serializable]
    public struct Icone
    {
        public GameObject binding;
        public InputActionReference action;
    }

    [SerializeField] GameObject prefabContextual;
    [SerializeField] GameObject prefabBinding;
    Transform parent;

    List<Icone> icones;

    public List<InputActionReference> inputActions;

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
        //Check for if the input already was created.
        for (int i = 0; i < icones.Count; i++)
        {
            if (icones[i].action == action) 
            {
                Debugar.Log("IS REPITED!");
                return;
            } 
        }

        //Check if parents exists. It does it after chechink the icone, to not do a null comparation on update.
        if (parent == null) parent = Instantiate(prefabContextual).transform.GetChild(0);

        //Create de binding
        GameObject binding = Instantiate(prefabBinding, parent);
        binding.GetComponent<Input_IconePerBinding>()?.MostrarIcone(action);

        //Adds the icon to the list.
        icones.Add(new Icone()
        {
            binding = binding,
            action = action
        });
 
    }

    public void Hide(InputActionReference inputAction)
    {
        Debugar.Log("Hide");
        for (int i = 0; i < icones.Count; i++)
        {
            if (icones[i].action == inputAction) 
            {
                Destroy(icones[i].binding);
                icones.RemoveAt(i);

                if (icones.Count != 0)
                    return;

                Destroy(parent.GetComponentInParent<Canvas>().gameObject);
                parent = null;
                break;
            } 
        }

    }

}
