using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    [SerializeField] private GameObject sceneGhost;
    [SerializeField] private GameManager sceneManager;

    public static GameObject ghost;
    public static GameManager manager;

    public static GameObject player;

    private void Awake()
    {
        ghost = sceneGhost;
        manager = sceneManager;
    }
}
