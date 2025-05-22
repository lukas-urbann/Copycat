using UnityEngine;

/// <summary>
/// Extenze StringReference, aby se typove dalo pristupovat k nazvu sceny
/// </summary>
[CreateAssetMenu]
public class SceneNameReference : StringVariable
{
    public SceneNameReference(string value) : base(value)
    {
        this.Value = value;
    }
}
