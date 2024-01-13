using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Threading;
using UnityEngine;

public class NPCsController : MonoBehaviour
{
    public static Dictionary<GameObject, int> npcs;

    private void Awake()
    {
        npcs = new Dictionary<GameObject, int>();

        foreach (Transform child in transform)
        {
            npcs.Add(child.gameObject, 100);
        }
    }

    public static void TakeDamage(GameObject npc)
    {
        if (npcs.ContainsKey(npc))
        {
            npcs.Remove(npc);
            Destroy(npc);
        }
    }
}
