using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Users;

public class Esdeveniment_Reconeixement : MonoBehaviour
{
    [SerializeField] Input_Reconeixement reconeixement;
    [SerializeField] Input_ReconeixementTipus[] buscats;

    [SerializeField] UnityEvent enTrobat;

    bool trobat;
    int index;

    private void OnEnable()
    {
        Buscar();
    }

    public void Buscar()
    {
        trobat = false;
        index = 0;

        while (index < buscats.Length && trobat == false)
        {
            if (reconeixement.actual == buscats[index]) trobat = true;
            index++;
        }

        if (trobat) enTrobat.Invoke();
    }
}
