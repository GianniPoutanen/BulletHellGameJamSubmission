using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IEquatable<Item>
{
    public string description = "This is an Item";
    public Sprite sprite;
    public int amount = 1;

    private bool pickup = false;
    private GameObject player;
    public float pickupSpeed = 3;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public Item(Item item)
    {
        description = item.description;
        sprite = item.sprite;
    }

    public void Pickup()
    {
        Inventory.AddToInventory(this);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (pickup)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, pickupSpeed);
            if (Vector2.Distance(this.transform.position, player.transform.position) < 0.1f)
            {
                Pickup();
            }
        }
        else if (Vector2.Distance(this.transform.position, player.transform.position) < 1f)
        {
            pickup = true;
        }
    }

    public bool Equals(Item other)
    {
        return other.sprite == this.sprite;
    }
}
