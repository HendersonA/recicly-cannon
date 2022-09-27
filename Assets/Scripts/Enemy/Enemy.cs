// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private Health health;
    private SpawnItemOverTime spawnItem;
    private NavMeshAgent navMeshAgent;

    [Header("Enemy")]
    [SerializeField] private bool isBoss = false;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float moveSpeed;
    public List<MaterialEnum> materialEnumList;

    [Header("Gold to Earn")]
    [SerializeField] private int scoreValue = 10;

    private Transform player;
    private Transform wall;
    private Transform target;

    void Awake()
    {
        health = GetComponent<Health>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        spawnItem = GetComponent<SpawnItemOverTime>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wall = GameObject.FindGameObjectWithTag("Wall").transform;
        target = player;
        RandomizeEnemyPresets();
    }

    void Update()
    {
        if (isBoss)
        {
            ChaseWall();
        }
        else
        {
            Chase();
        }
    }

    void OnEnable()
    {
        health.OnDeath += Die;
    }

    void OnDisable()
    {
        health.OnDeath -= Die;
    }

    private void DealDamage(Action<float> OnCallback)
    {
        float realDamage = damage * Time.deltaTime * 0.5f;
        OnCallback?.Invoke(realDamage);
    }

    private void Chase()
    {
        float distancePlayer = Vector3.Distance(player.position, transform.position);
        float distanceWall = Vector3.Distance(wall.position, transform.position);

        if (distancePlayer < distanceWall && target.Equals(wall))
        {
            target = player;
        }
        else if (distancePlayer > distanceWall && target.Equals(player))
        {
            target = wall;
        }

        navMeshAgent.SetDestination(target.position);
    }

    private void ChaseWall()
    {
        if (target.Equals(player))
        {
            target = wall;
        }

        navMeshAgent.SetDestination(target.position);
    }

    private void RandomizeEnemyPresets()
    {
        moveSpeed = UnityEngine.Random.Range(0.5f, 2f);
        navMeshAgent.speed = moveSpeed;
    }

    private void Die()
    {
        spawnItem.DropItem();
        Destroy(this.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            DealDamage(other.GetComponent<Health>().TakeDamage);
        }
        else if (other.CompareTag("Player"))
        {
            DealDamage(other.GetComponent<Health>().TakeDamage);
        }
    }

}