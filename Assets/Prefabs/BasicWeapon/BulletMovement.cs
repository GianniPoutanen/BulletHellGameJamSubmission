using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Header("Bullet Movement")]
    public Vector2 direction = Vector2.zero;
    public float speed = 3f;

    private float decayTimer = 2f;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += (Vector3)direction * speed * Time.deltaTime ;
    }
}
