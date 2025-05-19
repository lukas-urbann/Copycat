using UnityEngine;

[CreateAssetMenu]
public class StringVariable : ScriptableObject
{
    public StringVariable(string value)
    {
        this.Value = value;
    }
    
    public string Value;
}

