using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : EntityBase
{
    [Header("Stats")]
    public float damage;
    public float maxHealth;
    public float health;

    public float damageTickTime = 0.1f;
    public float damageTickTimer = 0f;

    [Header("Avoidance")]
    public float avoidanceRadius = 1f;
    public float avoidanceStrength = 0.1f;

    [Header("Speed")]
    public float chaseSpeed;

    private void Start()
    {
        Setup();
    }

    private void OnEnable()
    {
        Setup();
    }

    void Update()
    {
        damageTickTimer -= Time.deltaTime;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, avoidanceRadius);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject != this.gameObject)
            {
                if (collider.transform.tag == "Enemy" && Vector2.Distance(this.transform.position, collider.transform.position) < avoidanceRadius)
                {
                    // Avoidance
                    Vector2 avoidanceVelocity = (this.transform.position - collider.transform.position).normalized * avoidanceStrength * Time.deltaTime;
                    this.transform.position += new Vector3(avoidanceVelocity.x, avoidanceVelocity.y);
                }
            }
        }
        Vector2 chaseVelocity = (GameManager.Instance.playerCharacter.transform.position - this.transform.position).normalized * chaseSpeed * Time.deltaTime;
        this.transform.position += new Vector3(chaseVelocity.x, chaseVelocity.y);
    }

    public void Setup()
    {
        health = maxHealth;
    }

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player" && damageTickTimer <= 0)
        {
            GameManager.Instance.playerCharacter.GetComponent<PlayerBehaviour>().Damage(this.damage);
            PointPopUp.Create(other.transform.position + new Vector3(0, 0.3f, 0), this.damage);
            damageTickTimer = damageTickTime;
        }
    }
}
