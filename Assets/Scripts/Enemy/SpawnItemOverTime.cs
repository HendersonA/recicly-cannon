using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemOverTime : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemToSpawn;
    [SerializeField] private float timeToSpawn = 10.0f;

    void Start()
    {
        InvokeRepeating("DropItem", timeToSpawn, timeToSpawn);
    }

    public void DropItem()
    {
        foreach (var item in itemToSpawn)
        {
            Instantiate(item, this.transform.position, this.transform.rotation, this.transform.parent);
        }
    }
}