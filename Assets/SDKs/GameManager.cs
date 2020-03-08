using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private bool _gameActive;
    

    public bool gameActive { get => _gameActive; }

    // Start is called before the first frame update
    void Start()
    {
        _gameActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        _gameActive = true;
        SpawnManager.Instance.StartSpawning();
    }
    public void StopGame()
    {
        _gameActive = false;
        SpawnManager.Instance.EndSpawning();
    }
}
