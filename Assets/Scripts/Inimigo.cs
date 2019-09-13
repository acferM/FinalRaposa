using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    private float movimento = -2;
    private bool colidiu = false;
    public int vida = 4;
    private bool morto;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(movimento, 0);

        if (colidiu)
        {
            virar();
        }

        if (vida <= 0)
        {
            if (PlayerBehaviour.rageMode) GetComponent<Animator>().SetBool("DeadRage", true);
            else if (!PlayerBehaviour.rageMode) GetComponent<Animator>().SetBool("Dead", true);
            morto = true;
        }

        if (morto)
        {
            Destroy(this.GetComponent<BoxCollider2D>());
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }

    void virar()
    {
        movimento *= -1;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        colidiu = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            colidiu = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            colidiu = false;
        }
    }
}
