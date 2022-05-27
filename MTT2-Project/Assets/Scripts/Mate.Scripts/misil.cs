using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class misil : MonoBehaviour
{
    public int damage = 1;
    public float speed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        
    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Madera madera = hitInfo.GetComponent<Madera>();
        if (madera != null)
        {
            madera.TakeDamage(damage);
        }
        Destroy(gameObject);
        
    }
}
