using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables

    private Transform m_PlayerTransform;
    [SerializeField] private float m_Speed;
    private Vector3 m_TowardsPlayer;

    #endregion

    private void Start()
    {
        m_PlayerTransform = GameObject.Find("Player").GetComponent<Transform>();
        m_TowardsPlayer = m_PlayerTransform.position - transform.position;
        m_TowardsPlayer.Normalize();
    }

    private void Update()
    {
        Move();
        DestroyOffScreen();
    }

    private void Move()
    {
        transform.position += m_TowardsPlayer * m_Speed * Time.deltaTime;
    }

    private void DestroyOffScreen()
    {
        Vector3 cameraPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (cameraPosition.x < 0 || cameraPosition.x > Screen.width || cameraPosition.y < 0 || cameraPosition.y > Screen.height)
        {
            Destroy(gameObject);
        }
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
        if (collider.GetComponent<Player>())
        {
            collider.GetComponent<Player>().Damage();
            Destroy(gameObject);
        }
    }

    #endregion
}
