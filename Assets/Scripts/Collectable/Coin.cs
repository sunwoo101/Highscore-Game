using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    public override void Collected(GameObject gameObject)
    {
        gameObject.GetComponent<Player>().CoinsCollected++;
        
        base.Collected(gameObject);
    }
}
