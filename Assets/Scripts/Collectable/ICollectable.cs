using UnityEngine;

public interface ICollectable
{
    public void Destroy();
    public void Collected(GameObject gameObject);
}
