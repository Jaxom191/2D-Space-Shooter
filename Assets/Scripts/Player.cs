using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private int _score;
    public int score;
    [SerializeField]
    private float _xMin = -9f, _xMax = 9f, _yMin = -4.75f, _yMax = 0f;
    [SerializeField]
    private float _laserOffset = 1.25f;
    private Animator _anim;
    [SerializeField]
    private int _lives;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private bool _tripleShotActive;
    [SerializeField]
    private bool _speedBoost;
    [SerializeField]
    private bool _shields;
    private float _powerCooldown = 10;
    private float _canShoot = -1f;
    private float _shotDelay = .25f;
    private AudioSource _audioSource;
    private Collider2D m_Collider;

    public AudioClip powerUp;
    public AudioClip explotion;
    
    public GameObject[] _thusters;
    public GameObject[] _Damages;
    public float moveDirection;

    

    // Start is called before the first frame update
    void Start()
    {
        _lives = 3;
        _tripleShotActive = false;
        _speedBoost = false;
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        m_Collider = GetComponent<Collider2D>();
        m_Collider.enabled = true;
        _score = 0;
        
       

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovment();
        Shoot();
        moveDirection = Input.GetAxis("Horizontal");
        _anim.SetFloat("moveDirection", moveDirection);
       

       
        
    }

    void PlayerMovment()
    {
        if (_lives > 0)
        {
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), (Input.GetAxis("Vertical")));
            transform.Translate(direction * _speed * Time.deltaTime);
            transform.position = new Vector2((Mathf.Clamp(transform.position.x, _xMin, _xMax)), Mathf.Clamp(transform.position.y, _yMin, _yMax));
        }
        
    }

    void Shoot()
    {
              
        if (Input.GetKeyDown(KeyCode.Space) &&  Time.time > _canShoot && _lives > 0)
        {
            _canShoot = Time.time + _shotDelay;
            if (_tripleShotActive)
            {
                GameObject tripleShotLaser = PoolManager.Instance.GetPooledObject("TripleShotLaser");
                if (tripleShotLaser != null)
                {
                    tripleShotLaser.transform.position = new Vector2(transform.position.x, transform.position.y);
                    tripleShotLaser.transform.rotation = Quaternion.Euler(0, 0, 0);
                    tripleShotLaser.SetActive(true);
                    
                }
            }
            else
            {
                GameObject laser = PoolManager.Instance.GetPooledObject("Laser");
                if (laser != null)
                {
                    laser.transform.position = new Vector2(transform.position.x, transform.position.y + _laserOffset);
                    laser.transform.rotation = Quaternion.Euler(0, 0, 0);
                    laser.SetActive(true);
                }
            }         
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy_Laser")
        {
            if (_shields == true)
            {
                _shields = false;
                _shield.SetActive(false);
            }
            else
            {
                _lives --;
                UIManager.Instance.UpdateLives(_lives);

                switch (_lives)
                {
                    case 1:
                        _thusters[1].SetActive(false);
                        _Damages[1].SetActive(true);
                        break;
                    case 2:
                        _thusters[0].SetActive(false);
                        _Damages[0].SetActive(true);
                        break;
                    default:
                        break;
                }

                if (_lives <= 0)
                {
                    _lives = 0;
                    _audioSource.PlayOneShot(explotion, 1F);
                    _anim.SetInteger("Lives", _lives);
                    m_Collider.enabled = false;
                    foreach (var item in _Damages)
                    {
                        item.SetActive(false);
                    }
                    foreach (var item in _thusters)
                    {
                        item.SetActive(false);
                    }
                    GameManager.Instance.StopGame();
                }

            }
        }
        else
        {
            switch (other.gameObject.tag)
            {
                case "Shield":
                    _shield.SetActive(true);
                    _shields = true;
                    _audioSource.PlayOneShot(powerUp, 1F);
                    other.gameObject.SetActive(false);
                    break;
                case "Speed":
                    SpeedBoostActive();
                    _audioSource.PlayOneShot(powerUp, 1F);
                    other.gameObject.SetActive(false);
                    break;
                case "TripleShot":
                    TripleShotActive();
                    _audioSource.PlayOneShot(powerUp, 1F);
                    other.gameObject.SetActive(false);
                    break;

                default:
                    break;
            }
        }   
    }
    private void TripleShotActive()
    {
        _tripleShotActive = true;
        StartCoroutine(Timer(_powerCooldown,_tripleShotActive));
       
    }
    private void SpeedBoostActive()
    {
        _speedBoost = true;
        _speed = 10;
        _thusters[2].SetActive(true);
        StartCoroutine(Timer(_powerCooldown,_speedBoost));
    }
    IEnumerator Timer(float timer , bool powerUp)
    {
        yield return new WaitForSeconds(timer);

        if (powerUp == _speedBoost)
        {
            _thusters[2].SetActive(false);
            _speed = 5;
            _speedBoost = false;
        }
       else if (powerUp == _tripleShotActive)
        {
            _tripleShotActive = false;
        }
    }
    public void UpdateScore()
    {
        _score += Random.Range(5, 15);
        score = _score;
        UIManager.Instance.DisplayScore();
    }
}
