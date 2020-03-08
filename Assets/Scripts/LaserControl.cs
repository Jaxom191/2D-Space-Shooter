using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movment();
    }
    void Movment()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
            if (transform.position.y > 9 || transform.position.y < -6)
            {
                gameObject.SetActive(false);
            }
    }
}
