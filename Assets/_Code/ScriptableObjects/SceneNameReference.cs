using UnityEngine;

[CreateAssetMenu]
public class SceneNameReference : StringVariable
{
    public SceneNameReference(string value) : base(value)
    {
        this.Value = value;
    }
}
