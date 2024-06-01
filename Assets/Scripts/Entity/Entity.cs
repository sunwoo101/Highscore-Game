using System.Collections;
using UnityEngine;

// Components
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public abstract class Entity : MonoBehaviour, IDamagable
{
    #region Variables

    // Attributes
    [SerializeField] private float m_BaseHealth, m_BaseMovementSpeed, m_BetweenShotsDelay;
    private float m_CurrentHealth, m_ExtraMovementSpeed;
    public float ExtraMovementSpeed
    {
        get { return m_ExtraMovementSpeed; }
        set { m_ExtraMovementSpeed = value; }
    }

    // References
    private Rigidbody2D m_RigidBody;
    [SerializeField] protected GameObject m_ProjectilePrefab;

    // Input (Human and AI)
    protected Vector2 m_MovementInput;
    protected bool m_ShootInput;

    // Checks
    private bool m_IsShootReady;

    #endregion

    protected void Start()
    {
        // Attributes
        m_CurrentHealth = m_BaseHealth;

        // References
        m_RigidBody = GetComponent<Rigidbody2D>();

        // Checks
        m_IsShootReady = true;
    }

    protected void Update()
    {
        Input();
        Move();
        ShootInput();
    }

    /// <summary>
    /// Change m_MovementInput and m_ShootInput to control the entity
    /// </summary>
    protected abstract void Input();

    private void Move()
    {
        m_MovementInput.Normalize();
        Vector2 speed = m_MovementInput * (m_BaseMovementSpeed + m_ExtraMovementSpeed);
        m_RigidBody.velocity = speed;
    }

    private void ShootInput()
    {
        if (m_IsShootReady && m_ShootInput)
        {
            m_IsShootReady = false;
            Shoot();
            StartCoroutine(ShootCooldown());
        }
    }

    protected virtual void Shoot()
    {

    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(m_BetweenShotsDelay);

        m_IsShootReady = true;
    }

    public void Damage()
    {
        Death();
    }

    public void Damage(int damage)
    {
        if (m_CurrentHealth >= damage)
        {
            m_CurrentHealth -= damage;
        }
        else if (m_CurrentHealth > 0)
        {
            m_CurrentHealth = 0;
        }

        if (m_CurrentHealth == 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
