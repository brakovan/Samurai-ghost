using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    [SerializeField] private float minMoveSpeed = 0f;
    [SerializeField] private float maxMoveSpeed = 0f;
    [SerializeField] private float accelerationFactor = 0f;

    [SerializeField] private LayerMask raycastLayers;

    private void Update()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity, raycastLayers))
        {
            Vector3 pointToLook = hit.point;
            pointToLook.y = transform.position.y;

            float distanceToCursor = Vector3.Distance(transform.position, pointToLook);
            float targetSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, distanceToCursor * accelerationFactor);

            transform.position = Vector3.MoveTowards(transform.position, pointToLook, targetSpeed * Time.deltaTime);
            transform.LookAt(pointToLook);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            gameObject.SetActive(false);

            other.gameObject.GetComponent<NPCController>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            other.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            other.gameObject.GetComponent<PlayerController>().enabled = true;

            //Singleton.player = other.gameObject;
        }
    }
}
