using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    [SerializeField] private MaterialEnum materialEnum;
    public float damage;
    // public SFXScriptable sfx;
    public float lifetime = 2.0f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public bool HitByMaterial(MaterialEnum materialItem)
    {
        if (materialItem == MaterialEnum.Organico && materialEnum == MaterialEnum.Plastico)
        {
            return true;
        }
        else
        if (materialItem == MaterialEnum.Organico && materialEnum == MaterialEnum.Metal)
        {
            return true;
        }
        else
        if (materialItem == MaterialEnum.Plastico && materialEnum == MaterialEnum.Organico)
        {
            return true;
        }
        else
        if (materialItem == MaterialEnum.Metal && materialEnum == MaterialEnum.Organico)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            foreach (var material in other.GetComponent<Enemy>().materialEnumList)
            {
                if (HitByMaterial(material))
                    other.GetComponent<Health>().TakeDamage(damage);
            }

            Destroy(this.gameObject);
        }
    }
}