using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform m_TopLeftBounds, m_BottomRightBounds;
    [SerializeField] private GameObject m_CoinPrefab, m_SpeedBoostPrefab;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_SpeedBoostChance;
    private GameObject m_CurrentObject;
    private Transform m_PlayerTransform;

    #endregion

    private void Start()
    {
        m_PlayerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (!m_CurrentObject)
        {
            Vector2 position;

            do
            {
                position = new Vector2(Random.Range(m_TopLeftBounds.position.x, m_BottomRightBounds.position.x),
                                       Random.Range(m_TopLeftBounds.position.y, m_BottomRightBounds.position.y));
            } while (Vector2.Distance(m_PlayerTransform.position, position) <= 2);

            GameObject spawnObject;
            float randVal = Random.Range(0.0f, 1.0f);
            spawnObject = (randVal > 0 && randVal < m_SpeedBoostChance) ? m_SpeedBoostPrefab : m_CoinPrefab;
            m_CurrentObject = Instantiate(spawnObject, position, Quaternion.identity);
        }
    }
}
