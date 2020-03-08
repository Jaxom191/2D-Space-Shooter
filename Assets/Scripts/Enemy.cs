using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    private float _laserOffsetY = -1f;
    private float _laserOffsetX = .12f;
    private bool _destroyed;
    private Animator _anim;
    private Collider2D m_Collider;
    private AudioSource _audioSource;
    private Player _player; 
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(EnemyShoot());
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        m_Collider = GetComponent<Collider2D>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        m_Collider.enabled = true;
        _destroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
         EnemyMovment();
    }
    void EnemyMovment()
    {
        if (!_destroyed)
        {
            transform.Translate(Vector2.down * _speed * Time.deltaTime);
            if (transform.position.y < -6.5f)
            {
                gameObject.SetActive(false);
            }
        }
    }
    IEnumerator EnemyShoot()
    {
        if (gameObject.activeInHierarchy && _destroyed == !true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));
            GameObject laser1 = PoolManager.Instance.GetPooledObject("Enemy_Laser");
            if (laser1 != null)
            {
                laser1.transform.position = new Vector2(transform.position.x + _laserOffsetX, transform.position.y + _laserOffsetY);
                laser1.transform.rotation = Quaternion.Euler(180, 0, 0);
                laser1.SetActive(true);                
            }
            GameObject laser2 = PoolManager.Instance.GetPooledObject("Enemy_Laser");
            if (laser2 != null)
            {
                laser2.transform.position = new Vector2(transform.position.x + -_laserOffsetX, transform.position.y + _laserOffsetY);
                laser2.transform.rotation = Quaternion.Euler(180, 0, 0);
                laser2.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            _destroyed = true;
            _anim.SetBool("destroyed", _destroyed);
            StartCoroutine(Active());
        }
        else if (other.gameObject.tag == "Laser" || other.gameObject.tag == "TLaser")
        {            
            other.gameObject.SetActive(false);
            _destroyed = true;
            _anim.SetBool("destroyed", _destroyed);
            StartCoroutine(Active());
        }
    }
    IEnumerator Active() 
    {
        _player.UpdateScore();
        m_Collider.enabled = false;
        _audioSource.Play();
        yield return new WaitForSeconds(2.33f);
        gameObject.SetActive(false);
    }
}
