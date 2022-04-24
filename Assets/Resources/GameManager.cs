using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Sound Volumes")]
    [Range(0f, 1f)]
    public float soundEffectVolume;
    [Range(0f, 1f)]
    public float musicVolume;

    [Header("Game Speed")]
    [Range(0f, 2f)]
    public float gameSpeed;

    [Header("Player Global Stats")]
    public float damageMultiplyer = 1f;
    public float speedMultiplyer = 1f;
    public float cooldownMultiplyer = 1f;
    public float extraShots = 1f;


    [HideInInspector]
    public GameObject playerCharacter;

    #region Constructor
    private static GameManager _i;
    public static GameManager Instance
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameManager>("GameManager"));
                _i.playerCharacter = GameObject.FindGameObjectWithTag("Player");
            }

            return _i;
        }
    }
    #endregion Constructor

    #region Time Methosd

    // TODO smooth transition
    public void SetGameSpeed(float speed)
    {
        gameSpeed = speed;
    }

    #endregion Time Methosd

}
