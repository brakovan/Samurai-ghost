using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform ghostEscapePoint;

    private Vector3 whatToLookAt;
    private Rigidbody rb;

    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float radiusAttack = 0f;

    [SerializeField] private LayerMask layerNPC;
    [SerializeField] private LayerMask raycastLayers;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // MOVE

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontal + vertical, 0, vertical - horizontal).normalized * moveSpeed;

        // LOOK

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity, raycastLayers))
        {
            Vector3 pointToLook = hit.point;
            pointToLook.y = transform.position.y;

            transform.LookAt(pointToLook);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.zero;

            GetComponent<NPCController>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<PlayerController>().enabled = false;

            Singleton.ghost.transform.position = ghostEscapePoint.position;
            Singleton.ghost.SetActive(true);

            Singleton.player = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Singleton.player = gameObject;

            foreach (Collider hit in Physics.OverlapSphere(transform.position, radiusAttack, layerNPC))
            {
                if (hit.gameObject == Singleton.player)
                    continue;

                Vector3 directionToTarget = hit.transform.position - transform.position;
                directionToTarget.Normalize();

                if (Vector3.Dot(transform.TransformDirection(Vector3.forward), directionToTarget) > 0)
                {
                    NPCsController.TakeDamage(hit.gameObject);
                }
            }
        }
    }
}