using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBehaviour : MonoBehaviour
{
    [Header("Bullet Stats")]
    public bool noDecay = false;
    public int numHits = 1;
    public int currentHits;
    public float hitDelay = 0.3f;
    private float hitDelayTimer = 0;
    public float damage = 5;

    public List<GameObject> enemiesHit = new List<GameObject>();
    public List<float> hitTimers = new List<float>();

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
        UpdateTimers();
    }

    private List<int> removeFromList = new List<int>();

    private void UpdateTimers()
    {
        for (int i = 0; i < hitTimers.Count; i++)
        {
            if (hitTimers[i] >= 0)
            {
                hitTimers[i] -= Time.deltaTime;
                if (hitTimers[i] <= 0)
                {
                    removeFromList.Add(i);
                }
            }
        }

        while (removeFromList.Count > 0)
        {
            enemiesHit.RemoveAt(removeFromList[0]);
            hitTimers.RemoveAt(removeFromList[0]);

            for (int i = 0; i < removeFromList.Count; i++)
            {
                if (removeFromList[0] < removeFromList[i])
                {
                    removeFromList[i]--;
                }
            }
            removeFromList.RemoveAt(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && !enemiesHit.Contains(other.gameObject))
        {
            Hit(other.GetComponent<EnemyBehaviour>());
            enemiesHit.Add(other.gameObject);
            hitTimers.Add(hitDelay);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy" && !enemiesHit.Contains(other.gameObject))
        {
            Hit(other.GetComponent<EnemyBehaviour>());
            enemiesHit.Add(other.gameObject);
            hitTimers.Add(hitDelay);
        }
    }

    public void Hit(EnemyBehaviour enemy)
    {
        float dmg = this.damage * GameManager.Instance.damageMultiplyer;

        PointPopUp.Create(this.transform.position, dmg);
        enemy.Damage(dmg);
        this.currentHits--;
        if (this.currentHits <= 0 && !noDecay)
        {
            this.gameObject.SetActive(false);
        }
    }
}

