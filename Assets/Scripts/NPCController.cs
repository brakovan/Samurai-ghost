using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private float radiusChasing = 7f;
    [SerializeField] private float radiusAttacking = 3f;

    [SerializeField] private int damage = 10;

    [SerializeField] private LayerMask layerBuilding;
    [SerializeField] private LayerMask layerNPC;

    [SerializeField] private GameObject weapon;
    private bool is_attack = false;

    private NavMeshAgent agent;
    private GameObject target;
    private Coroutine attack;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = radiusAttacking;
    }

    private void Update()
    {
        // BUILDING

        float shortestDistance = Mathf.Infinity;

        foreach (GameObject building in BuildingController.buildings.Keys)
        {
            float distance = Vector3.Distance(transform.position, building.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                target = building;
            }
        }

        // PLAYER

        foreach (Collider hit in Physics.OverlapSphere(transform.position, radiusChasing, layerNPC))
        {
            if (hit.gameObject == Singleton.player)
            {
                target = hit.gameObject;
            }
        }

        // ATTACK AND MOVE

        if (target == null) return;

        agent.SetDestination(target.GetComponent<Collider>().ClosestPoint(transform.position));

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (attack == null)
            {
                attack = StartCoroutine(AttackRoutine(target));
            }
            transform.LookAt(target.transform.position);
        }
    }

    private IEnumerator AttackRoutine(GameObject target)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        foreach (Collider hit in Physics.OverlapSphere(transform.position, radiusAttacking, layerNPC))
        {
            if (target == hit.gameObject) is_attack = true;
        }
        foreach (Collider hit in Physics.OverlapSphere(transform.position, radiusAttacking, layerBuilding))
        {
            if (target == hit.gameObject) is_attack = true;
        }

        if (is_attack)
        {
            if (target.layer == LayerMask.NameToLayer("Building"))
            {
                BuildingController.TakeDamage(target, damage);
            }
            if (target.layer == LayerMask.NameToLayer("NPC"))
            {
                Destroy(target);
                Singleton.manager.ToggleCanvas();
            }
        }

        is_attack = false;
        attack = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radiusChasing);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAttacking);
    }
}

   
