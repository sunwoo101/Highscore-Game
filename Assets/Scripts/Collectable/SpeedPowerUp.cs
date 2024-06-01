using UnityEngine;

public class SpeedPowerUp : Collectable
{
    #region Variables

    [SerializeField] private float m_ExtraMovementSpeedIncrement;

    #endregion

    public override void Collected(GameObject gameObject)
    {
        gameObject.GetComponent<Player>().CoinsCollected++;
        gameObject.GetComponent<Player>().ExtraMovementSpeed += m_ExtraMovementSpeedIncrement;
        
        base.Collected(gameObject);
    }
}
