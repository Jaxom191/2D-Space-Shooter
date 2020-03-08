using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _Speed = 8;
    private Animator _anim;
    private bool _destroyed;
    private Collider2D m_Collider;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        m_Collider = GetComponent<Collider2D>();
        m_Collider.enabled = true;
        _destroyed = false;
        _audioSource = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    void Rotate()
    {
        transform.Rotate((Vector3.forward) * _Speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            
            _destroyed = true;
            _anim.SetBool("destroyed", _destroyed);
            other.gameObject.SetActive(false);
            StartCoroutine(Active());
            
        }
    }
    IEnumerator Active()
    {
        m_Collider.enabled = false;
        _audioSource.Play();
        yield return new WaitForSeconds(2.33f);
        GameManager.Instance.StartGame();
        Destroy(gameObject);
    }
}
