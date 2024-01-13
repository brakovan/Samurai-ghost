using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public static Dictionary<GameObject, int> buildings;

    private void Awake()
    {
        buildings = new Dictionary<GameObject, int>();

        foreach (Transform child in transform)
        {
            buildings.Add(child.gameObject, 100);
        }
    }

    public static void TakeDamage(GameObject building, int damage)
    {
        Debug.Log(building.name);
        if (buildings.ContainsKey(building))
        {
            buildings[building] -= damage;

            if (buildings[building] <= 0)
            {
                buildings.Remove(building);

                building.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
                building.transform.localScale = new Vector3(building.transform.localScale.x, building.transform.localScale.y / 2, building.transform.localScale.z);
                building.transform.position = new Vector3(building.transform.position.x, building.transform.position.y / 2, building.transform.position.z);
            }

            if (buildings.Count == 0)
            {
                Singleton.manager.ToggleCanvas();
            }
        }
    }
}
