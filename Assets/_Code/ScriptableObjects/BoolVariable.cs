using UnityEngine;

/// <summary>
/// Nese bool hodnotu
/// </summary>
[CreateAssetMenu]
public class BoolVariable : ScriptableObject
{
    public BoolVariable(bool value)
    {
        this.Value = value;
    }
    
    public bool Value;
}