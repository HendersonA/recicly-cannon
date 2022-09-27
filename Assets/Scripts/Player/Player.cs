using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    private Health health;

    [SerializeField] private GameObject iconLife;
    [SerializeField] private Transform content;
    [SerializeField] private int lifeCount;

    public GameObject[] lifeIconList;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Start()
    {
        lifeIconList = new GameObject[lifeCount];

        SpawnImages(lifeIconList, content, iconLife);
    }

    public void SpawnImages(GameObject[] list, Transform target, GameObject prefab)
    {
        for (int i = 0; i < lifeCount; i++)
        {
            GameObject instance = Instantiate(prefab, target.position, target.rotation);
            instance.transform.SetParent(target);
            list[i] = instance;
        }
    }

    public void DestroyImage(GameObject[] listImage)
    {
        if (lifeCount >= 0)
        {
            GameObject lastImage = listImage.Last();
            lifeCount--;
            Destroy(lastImage);
        }
    }

    private void TakeDamage(float currentHealth, float maxHealth)
    {
        DestroyImage(lifeIconList);
    }

    void OnEnable()
    {
        health.OnHealth += TakeDamage;
    }
}
