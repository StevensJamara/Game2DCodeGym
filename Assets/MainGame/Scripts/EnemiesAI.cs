using JetBrains.Annotations;
using UnityEngine;

[AddComponentMenu("MainGame/EnemiesAI")]

public class EnemiesAI : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform PointA;
    public Transform PointB;
    public float speed = 2f;
    public Transform target;
    public float minDistance = 0.2f;

    private Vector3 nextPosition;
    private Rigidbody2D enemyRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        target = PointA;    
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        if (Vector2.Distance(transform.position, target.position) < minDistance)
        {
            target = target == PointA ? PointB : PointA;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        enemyRb.linearVelocity = dir * speed;
    }

    private void OnDrawGizmos()
    {
        if (PointA != null && PointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(PointA.position, PointB.position);
        }
    }
}
