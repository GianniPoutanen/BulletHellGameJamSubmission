using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsBehaviour : MonoBehaviour
{
    private const int BASIC_BULLET = 0;
    private void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateSwords();
        ShootAtClosest();
    }

    #region Revolving Swords

    [Header("Revolving Swords")]
    public GameObject childSwordHolder;
    public float swordSpinSpeed;
    private List<GameObject> swords = new List<GameObject>();

    public void UpdateSwords(int numSwords)
    {
        foreach (Transform sword in childSwordHolder.transform)
        {
            sword.gameObject.SetActive(false);
        }
        Quaternion savedRotation = childSwordHolder.transform.rotation;
        childSwordHolder.transform.rotation = new Quaternion(0, 0, 0, 0);

        float rotationAmount = 360f / numSwords;
        for (int i = 1; i < numSwords + 1; i++)
        {
            Transform sword = childSwordHolder.transform.GetChild(i - 1);
            sword.gameObject.SetActive(true);
            sword.transform.position = this.transform.position + new Vector3(0f, 2f);
            sword.transform.rotation = Quaternion.Euler(0, 0, sword.transform.rotation.eulerAngles.z - (rotationAmount * i) + rotationAmount);
            childSwordHolder.transform.rotation = Quaternion.Euler(0, 0, rotationAmount * i);
        }
    }

    public void RotateSwords()
    {
        childSwordHolder.transform.rotation = Quaternion.Euler(0, 0, (swordSpinSpeed + childSwordHolder.transform.rotation.eulerAngles.z));
    }

    #endregion Revolving Swords


    #region Closest Killer


    private float closestShotDelay = 0.1f;
    private float closestShotDelayTimer = 0f;
    private float closestShotCooldown = 2f;
    private float closestShotCooldownTimer = 1f;
    private float currentShots = 3;
    private float numShots = 3;
    public void ShootAtClosest()
    {
        //if (closestShotCooldownTimer <= 0)
        {
            if (closestShotDelayTimer <= 0)
            {
                GameObject newBullet = GameAssets.Instance.GetObject(GameAssets.Instance.bullets[BASIC_BULLET]);
                newBullet.transform.position = this.transform.position;
                newBullet.GetComponent<BulletMovement>().direction = (GetClosestEnemy().transform.position - this.transform.position).normalized;
                closestShotDelayTimer = closestShotDelay;
            }
            else
            {
                currentShots--;
                if (currentShots == 0)
                {
                    closestShotCooldownTimer = closestShotCooldown;
                    currentShots = numShots + GameManager.Instance.extraShots;
                }
                else
                {
                    closestShotDelayTimer -= Time.deltaTime;
                }
            }
        }
        // else
        {
            //    closestShotCooldownTimer -= Time.deltaTime;
        }
    }

    #endregion Closest Killer
    private GameObject closestEnemyGizmoObj;
    public GameObject GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = float.MaxValue;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            if (distance > Vector2.Distance(enemy.transform.position, this.transform.position))
            {
                closestEnemy = enemy;
                distance = Vector2.Distance(enemy.transform.position, this.transform.position);
            }
        }
        if (closestEnemyGizmoObj != null)
            closestEnemyGizmoObj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        closestEnemyGizmoObj = closestEnemy;
        if (closestEnemyGizmoObj != null)
            closestEnemyGizmoObj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
        return closestEnemy;
    }

    public void SpawnOnRandomEnemy(int numSpawn)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < numSpawn; i++)
        {
            GameObject newBullet = GameAssets.Instance.GetObject(GameAssets.Instance.bullets[BASIC_BULLET]);
            newBullet.transform.position = enemies[((int)(Random.value * (i + 1000f))) % enemies.Length].transform.position;
        }
    }
}
