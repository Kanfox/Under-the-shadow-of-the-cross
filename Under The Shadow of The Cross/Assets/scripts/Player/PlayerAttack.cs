using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ataque Fraco (botão direito do mouse)")]
    public float weakAttackRange = 1f;
    public float weakAttackOffsetX = 1f;
    public float weakAttackOffsetY = 0f;
    public int weakAttackDamage = 10;
    public float weakAttackHitInterval = 0.5f;

    [Header("Ataque Médio (botão esquerdo do mouse)")]
    public float mediumAttackRange = 1.5f;
    public float mediumAttackOffsetX = 1.5f;
    public float mediumAttackOffsetY = 0f;
    public int mediumAttackDamage = 5;
    public float mediumAttackHitInterval = 0.5f;

    [Header("Camadas inimigas")]
    public LayerMask enemyLayerMask;

    private HashSet<GameObject> enemiesHitThisAttack = new HashSet<GameObject>();
    private float lastHitTimeWeak = 0f;
    private float lastHitTimeMedium = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(PerformAttack(weakAttackOffsetX, weakAttackOffsetY, weakAttackRange, weakAttackDamage, weakAttackHitInterval, "Weak"));
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PerformAttack(mediumAttackOffsetX, mediumAttackOffsetY, mediumAttackRange, mediumAttackDamage, mediumAttackHitInterval, "Medium"));
        }
    }

    private IEnumerator<WaitForSeconds> PerformAttack(float offsetX, float offsetY, float range, int damage, float hitInterval, string attackType)
    {
        enemiesHitThisAttack.Clear();
        float attackStartTime = Time.time;
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if ((attackType == "Weak" && Time.time >= lastHitTimeWeak + hitInterval) ||
                (attackType == "Medium" && Time.time >= lastHitTimeMedium + hitInterval))
            {
                DoDamageInRange(offsetX, offsetY, range, damage, attackType);

                if (attackType == "Weak") lastHitTimeWeak = Time.time;
                else lastHitTimeMedium = Time.time;
            }

            yield return null;
        }
    }

    private void DoDamageInRange(float offsetX, float offsetY, float range, int damage, string attackType)
    {
        int direction = transform.localScale.x >= 0 ? 1 : -1;
        Vector3 attackPos = transform.position + new Vector3(offsetX * direction, offsetY, 0f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos, range, enemyLayerMask);
        foreach (var hit in hits)
        {
            if (hit != null && hit.CompareTag("Enemy"))
            {
                GameObject enemyGO = hit.gameObject;
                if (!enemiesHitThisAttack.Contains(enemyGO))
                {
                    NPCVida npc = enemyGO.GetComponent<NPCVida>();
                    if (npc != null)
                    {
                        npc.TakeDamage(damage);
                        enemiesHitThisAttack.Add(enemyGO);
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        int direction = transform.localScale.x >= 0 ? 1 : -1;
        Vector3 weakAttackPos = transform.position + new Vector3(weakAttackOffsetX * direction, weakAttackOffsetY, 0f);
        Vector3 mediumAttackPos = transform.position + new Vector3(mediumAttackOffsetX * direction, mediumAttackOffsetY, 0f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(weakAttackPos, weakAttackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(mediumAttackPos, mediumAttackRange);
    }
}
