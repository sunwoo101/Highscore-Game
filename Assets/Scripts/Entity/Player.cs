using UnityEngine;

public class Player : Entity
{
    #region Variables

    private int m_CoinsCollected;
    public int CoinsCollected
    {
        get { return m_CoinsCollected; }
        set { m_CoinsCollected = value; }
    }

    #endregion

    protected override void Input()
    {
        m_MovementInput.x = UnityEngine.Input.GetAxisRaw("Horizontal");
        m_MovementInput.y = UnityEngine.Input.GetAxisRaw("Vertical");
    }

    // Doing collisions like this makes sure collision works with objects with or without rigidbodies
    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollision(GameObject collider)
    {
        if (collider.GetComponent<Collectable>())
        {
            collider.GetComponent<Collectable>().Collected(gameObject);
        }
    }

    #endregion

    protected override void Death()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
    }
}
