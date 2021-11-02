using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/Inputs/Bindings Guardats", fileName = "Guardats")]
public class Input_BindingsGuardats : ScriptableObject
{
    Dictionary<string,string> bindings;
    [SerializeField] List<string> keys;
    [SerializeField] List<string> values;

    /*public Dictionary<string, string> Bindings
    {
        get
        {
            if (bindings == null) bindings = new Dictionary<string, string>();
            return bindings;
        }
    }*/
    public List<string> Keys
    {
        get
        {
            if (keys == null) keys = new List<string>();
            return keys;
        }
    }
    public List<string> Values
    {
        get
        {
            if (values == null) values = new List<string>();
            return values;
        }
    }

    int index = 0;
    bool trobat = false;

    public void Add(string key, string binding)
    {
        //Bindings.Add(key, binding);

        Keys.Add(key);
        Values.Add(binding);
    }
    public void Replace(string key, string binding)
    {
        //Bindings[key] = binding;

        index = 0;
        trobat = false;
        while (index < Keys.Count && !trobat)
        {
            if (Keys[index] == key) trobat = true;
            else index++;

            if (trobat)
            {
                Values[index] = binding;
            }
        }
    }
    public void Remove(string key)
    {
        //Bindings.Remove(key);

        index = 0;
        trobat = false;
        while(index < Keys.Count && !trobat)
        {
            if (Keys[index] == key) trobat = true;
            else index++;

            if(trobat)
            {
                Values.RemoveAt(index);
                Keys.RemoveAt(index);
            }
            
        }

    }
    public void RemoveAll()
    {
        //Bindings.Clear();

        Keys.Clear();
        Values.Clear();
    }

}
