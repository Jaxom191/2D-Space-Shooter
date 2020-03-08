using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField]
    private bool _gameActive = false;
    private int _selected;
    private string _selectedTag;
    // Start is called before the first frame update
    void Start()
    {

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemy()
    {
        while (_gameActive == true)
        {
            GameObject enemy = PoolManager.Instance.GetPooledObject("Enemy");
            if (enemy != null)
            {
                enemy.transform.position = new Vector2(Random.Range(-9f, 9f), 7f);
                enemy.transform.rotation = transform.rotation;
                yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
                enemy.SetActive(true);
                
            }
        }
    }
    IEnumerator SpawnPowerUps()
    {

         while (_gameActive == true)
        {
            _selected = Random.Range(1, 4);
            switch (_selected)
            {
                case 1: _selectedTag = "Shield";
                    break;
                case 2: _selectedTag = "Speed";
                    break;
                case 3: _selectedTag = "TripleShot";
                    break;
                default:
                    break;
            }
            GameObject powerUp = PoolManager.Instance.GetPooledObject(_selectedTag);
            if (powerUp != null)
            {
                powerUp.transform.position = new Vector2(Random.Range(-9f, 9f), 7f);
                powerUp.transform.rotation = transform.rotation;
                yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
                powerUp.SetActive(true);
                
            }
        }

    }
    public void StartSpawning()
    {
        _gameActive = GameManager.Instance.gameActive;
        StartCoroutine(SpawnPowerUps());
        StartCoroutine(SpawnEnemy());
    }
    public void EndSpawning()
    {
        _gameActive = GameManager.Instance.gameActive;
        StopAllCoroutines();
    }
}
