using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    protected override void Input()
    {
        m_MovementInput = Vector2.zero;
        m_ShootInput = true;
    }

    protected override void Shoot()
    {
        Instantiate(m_ProjectilePrefab, transform.position, Quaternion.identity);
    }
}
