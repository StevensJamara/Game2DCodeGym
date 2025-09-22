using System.Collections;
using UnityEngine;

[AddComponentMenu("MainGame/PlayerAttack")]

public class PlayerAttack : MonoBehaviour
{
    public float attackRadius = 0.5f;
    public Transform pointAttack;
    public float attackRange = 0.5f;
    public float nextAttackTime = 0f;
    public float timeDelay = 0.2f;
    public LayerMask enemyLayers;
    public int damageCaused = 20;

    private Animator anim;
    private int isAttackedAnimationID;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        isAttackedAnimationID = Animator.StringToHash("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger(isAttackedAnimationID);
            GetKeyR();
        }
    }

    #region Play Attack function and Animation
    private bool GetKeyR()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + timeDelay;
            StartCoroutine(Attack());
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator Attack()
    {
        if (GetKeyR())
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(pointAttack.position, attackRange, enemyLayers);
            for (int i = 0; i < hitEnemies.Length; i++)
            {
                hitEnemies[i].GetComponent<ICanTakeDamage>()?.TakeDamage(damageCaused, pointAttack.position, gameObject);
            }
        }
        yield return new WaitForSeconds(0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        if (pointAttack == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pointAttack.position, attackRange);
    }
    #endregion
}
