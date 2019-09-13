using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    public Collider2D[] colliders = new Collider2D[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), colliders);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null && colliders[i].gameObject.CompareTag("Enemy"))
            {
                colliders[i].gameObject.GetComponent<Inimigo>().vida -= PlayerBehaviour.dano;
            }
        }
    }
}
