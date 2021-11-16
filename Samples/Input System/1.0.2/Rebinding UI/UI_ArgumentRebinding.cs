using UnityEngine;

public class UI_ArgumentRebinding : MonoBehaviour
{
    string argument;
    public string Argument { get => $"''{argument}''"; set => argument = value; }
}
