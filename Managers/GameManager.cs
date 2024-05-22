using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PoolManager poolManager;
    [SerializeField] private PhaseManager phaseManager;
    public PoolManager Pool => poolManager;
    public PhaseManager PhaseManager => phaseManager;
    [field:SerializeField]public GameObject GameOverUI { get; private set; }
    [field:SerializeField]public GameObject GameClearUI { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AudioManager.instance.PlayBgm(Bgm.MainBgm, true);
    }
}