using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    private Player _player;
    [SerializeField]
    private Text _score;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _livesSprites;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameOver.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && GameManager.Instance.gameActive == false)
        {
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void DisplayScore()
    {
        _score.text = "Score: " + _player.score.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _livesSprites[currentLives];
        if (currentLives == 0)
        {
            _gameOver.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOver());
        }
    }
    IEnumerator GameOver()
    {
        while (true)
        {
            _gameOver.text = "Game Over !";
            yield return new WaitForSeconds(.4f);
            _gameOver.text = "";
            yield return new WaitForSeconds(.4f);
        }
    }
}
