using UnityEngine;

[CreateAssetMenu]
public class BoolVariable : ScriptableObject
{
    public BoolVariable(bool value)
    {
        this.Value = value;
    }
    
    public bool Value;
}