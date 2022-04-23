using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    [Header("Bullet Stats")]
    public int numHits = 1;
    public int currentHits;
    public float hitDelay = 0.3f;
    private float hitDelayTimer = 0;
    public float damage = 5;

    private void Awake()
    {
        Setup();
    }
    private void OnEnable()
    {
        Setup();
    }

    public void Setup()
    {
        currentHits = numHits;
        hitDelayTimer = 0;
    }

    public void Update()
    {
        if (hitDelayTimer >= 0)
        {
            hitDelayTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" )
        {
            Hit(other.GetComponent<EnemyBehaviour>());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Hit(other.GetComponent<EnemyBehaviour>());
        }
    }

    public void Hit(EnemyBehaviour enemy)
    {
        PointPopUp.Create(this.transform.position, this.damage);
        enemy.Damage(this.damage);
        this.currentHits--;
        if(this.currentHits <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
