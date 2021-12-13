using UnityEngine;

public class UI_ArgumentRebinding : MonoBehaviour
{
    string argument;
    public string Argument { get => ($"''{argument}''").ToUpper(); set => argument = value; }
    public override string ToString() => Argument;
}
