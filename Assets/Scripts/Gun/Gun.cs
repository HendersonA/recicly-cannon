using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    public enum ShootState
    {
        Ready,
        Shooting,
        Empty,
        Reloading
    }

    [Header("Magazine")]
    public GameObject round;
    [SerializeField] private int ammunition;

    [Range(0.5f, 10)][SerializeField] private float reloadTime;

    [Header("Shooting")]
    [Range(0.25f, 25)][SerializeField] private float fireRate;
    [SerializeField] private int roundsPerShot;

    [Range(0.5f, 1000)][SerializeField] private float roundSpeed;
    [Range(0, 45)][SerializeField] private float maxRoundVariation;

    private float nextShootTime = 0;
    private int remainingAmmunition;
    private float muzzleOffset;
    private ShootState shootState = ShootState.Ready;

    void Start()
    {
        muzzleOffset = round.GetComponent<Renderer>().bounds.extents.z;
        remainingAmmunition = ammunition;
    }

    void Update()
    {
        switch (shootState)
        {
            case ShootState.Shooting:
                if (Time.time > nextShootTime)
                {
                    shootState = ShootState.Ready;
                }
                break;
            case ShootState.Reloading:
                if (Time.time > nextShootTime)
                {
                    remainingAmmunition = ammunition;
                    shootState = ShootState.Ready;
                }
                break;
        }
    }

    void OnEnable()
    {
        TakeItem.OnGetItem += SelectRound;
    }

    void OnDisable()
    {
        TakeItem.OnGetItem -= SelectRound;
    }

    public void Shoot()
    {
        if (shootState == ShootState.Ready)
        {
            for (int i = 0; i < roundsPerShot; i++)
            {
                GameObject spawnedRound = Instantiate(
                    round,
                    transform.position + transform.forward * muzzleOffset,
                    transform.rotation
                );

                spawnedRound.transform.Rotate(new Vector3(
                    Random.Range(-1f, 1f) * maxRoundVariation,
                    Random.Range(-1f, 1f) * maxRoundVariation,
                    0
                ));

                Rigidbody rb = spawnedRound.GetComponent<Rigidbody>();
                rb.velocity = spawnedRound.transform.forward * roundSpeed;
            }

            remainingAmmunition--;
            if (remainingAmmunition > 0)
            {
                nextShootTime = Time.time + (1 / fireRate);
                shootState = ShootState.Shooting;
            }
            else
            {
                shootState = ShootState.Empty;
            }
        }
    }

    public void Reload()
    {
        if (shootState == ShootState.Ready)
        {
            nextShootTime = Time.time + reloadTime;
            shootState = ShootState.Reloading;
        }
    }

    public int Ammunition
    {
        get
        {
            return remainingAmmunition;
        }

        set
        {
            remainingAmmunition = value;
        }
    }

    private void SelectRound(ItemScriptable itemScriptable)
    {
        round = itemScriptable.itemBullet;
    }
}