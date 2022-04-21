using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : EntityBase
{
    [Header("Stats")]
    public float maxHealth;
    private float currentHealth;
    public float walkSpeed;

    private Rigidbody2D rigidbody;
    private Animator anim;

    void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        GameManager.Instance.playerCharacter = this.gameObject;
    }

    public override void FixedUpdate() 
    {
        base.FixedUpdate();
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.3f)
        {
            this.transform.localScale = new Vector3(Mathf.Sign(Input.GetAxis("Horizontal")), 1, 1);
        }

        if (this.rigidbody.velocity.magnitude > 0.1f)
        {
            anim.Play("Walking");
        }
        else
        {
            anim.Play("Idle");
        }

        this.rigidbody.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * walkSpeed, 0.8f),
                                             Mathf.Lerp(0, Input.GetAxis("Vertical") * walkSpeed, 0.8f));
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            /// death
        }
    }

    public void Heal(float amount)
    {
        if (currentHealth + amount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
    }
}
