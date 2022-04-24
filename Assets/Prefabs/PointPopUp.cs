using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointPopUp : MonoBehaviour
{
    private float disappearTimer;
    public Color textColour;
    private float disappearSpeed = 3f;
    private Vector2 velocity = new Vector2(0, 10);
    private TextMeshPro textmesh;

    public static PointPopUp Create(Vector3 position, float PointsAmount)
    {
        Transform PointsPopUpTransform = GameAssets.Instance.GetObject(GameAssets.Instance.textPopup).transform;// Instantiate(GameAssets.Instance.textPopup.transform, position, Quaternion.identity);
        PointsPopUpTransform.position = position;
        PointPopUp pointPopUp = PointsPopUpTransform.GetComponent<PointPopUp>();
        pointPopUp.Setup(PointsAmount);
        pointPopUp.gameObject.SetActive(true);
        pointPopUp.disappearTimer = 0.2f;
        return pointPopUp;
    }

    private void Awake()
    {
        textmesh = transform.GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        textColour.a = 1f;
        textmesh.color = textColour;
    }

    public void Setup(float PointAmount, float disSpeed, Vector2 vel, Color colour)
    {
        Setup(PointAmount, disSpeed,vel);
        textColour = colour;
        textmesh.color = textColour;
    }

    public void Setup(float PointAmount, float disSpeed, Vector2 vel)
    {
        velocity = vel;
        Setup(PointAmount, disSpeed);
    }
    public void Setup(float PointAmount, float disSpeed)
    {
        this.disappearSpeed = disSpeed;
        Setup(PointAmount);
    }
    public void Setup(float PointAmount)
    {
        textColour = Color.white;
        textmesh.SetText(PointAmount.ToString());
        textmesh.color = textColour;
        textColour = textmesh.color;
        disappearSpeed = 3f;
        velocity = new Vector2(0, 10);
    }
    private void Update()
    {
        transform.position += new Vector3(velocity.x, velocity.y) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer <= 0)
        {
            // Start dissappearing
            textColour.a -= disappearSpeed * Time.deltaTime;
            textmesh.color = textColour;
            if (textColour.a <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}