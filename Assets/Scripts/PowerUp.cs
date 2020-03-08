using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
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
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        if (transform.position.y < -6.5f)
        {
            gameObject.SetActive(false);
        }
    }
}
