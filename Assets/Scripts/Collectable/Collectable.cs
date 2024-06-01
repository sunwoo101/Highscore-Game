using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    public virtual void Collected(GameObject gameObject)
    {
        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
