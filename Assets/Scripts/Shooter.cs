using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;
    [SerializeField] bool useAI;

    public bool isFiring;

    Coroutine firingCoroutine;

    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }


    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContiniously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }
    IEnumerator FireContiniously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifetime);
            yield return new WaitForSeconds(firingRate);
        }
    }
}
