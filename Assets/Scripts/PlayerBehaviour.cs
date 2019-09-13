using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator an;
    private AnimatorStateInfo animatorState;
    private int noChão;
    private float movimento;
    private float textoBarra;
    private float tempoSumirAtaque = 0.15f;
    private float tempoAtivo;
    private float percaBarra = 0.001f;
    private float timezinA = 0.2f;
    private float timeA;
    private bool attackCan;

    public static int dano = 1;
    public int vida = 6;
    public Text txtVida;
    public GameObject rawImage;
    public GameObject Player;
    public GameObject Ataque;
    public Image barraRage;
    public Text textoBarraRage;
    public static bool rageMode;
    public GameObject canvas;

    private void Awake()
    {
        rawImage.SetActive(false);

        barraRage.fillAmount = 0;

        Ataque.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();

        noChão = 2;

        vida = 6;

        rageMode = false;

        InvokeRepeating("heal", 0f, 3f);

        attackCan = true;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        // Movimentação
        movimento = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(movimento * 2, rb.velocity.y);

        if (movimento > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            an.SetBool("Andando", true);
        }
        else if (movimento < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            an.SetBool("Andando", true);
        }
        else
        {
            an.SetBool("Andando", false);
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.UpArrow) && noChão > 0 || Input.GetKeyDown(KeyCode.W) && noChão > 0)
        {
            rb.AddForce(new Vector2(0, 200));
            rb.velocity = new Vector2(rb.velocity.x, 0);
            noChão--;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && movimento > 0)
        {
            rb.AddForce(new Vector2(600, 0));
            an.SetBool("Dasheando", true);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && movimento < 0)
        {
            rb.AddForce(new Vector2(-600, 0));
            an.SetBool("Dasheando", true);
        }

        // Vida
        txtVida.text = vida.ToString();

        if (vida >= 6)
        {
            vida = 6;
        }

        // Animaçao dash
        animatorState = an.GetCurrentAnimatorStateInfo(0);

        if (animatorState.nameHash == -1075164626)
        {
            an.SetBool("Dasheando", false);
        }

        // Barra rage
        if (barraRage.fillAmount == 1)
        {
            rageMode = true;
            percaBarra = 0.007f;
        }
        
        if (barraRage.fillAmount > 0)
        {
            barraRage.fillAmount -= percaBarra;
        }

        if (barraRage.fillAmount == 0)
        {
            textoBarraRage.gameObject.SetActive(false);
            rageMode = false;
            percaBarra = 0.001f;
            dano = 1;
        }
        else
        {
            textoBarraRage.gameObject.SetActive(true);

        }

        textoBarra = barraRage.fillAmount * 100;

        textoBarraRage.text = Mathf.Round(textoBarra).ToString();

        // Rage mode
        if (rageMode)
        {
            rawImage.SetActive(true);
            Player.transform.localScale = new Vector3(4.14f, 4.14f, 4.14f);
            dano = 4;
        }
        else
        {
            rawImage.SetActive(false);
            dano = 1;
            Player.transform.localScale = new Vector3(2.028415f, 2.028415f, 2.028415f);
        }


        // Ataque  
        if (Input.GetKeyDown(KeyCode.X) && attackCan)
        {
            Ataque.SetActive(true);

            tempoAtivo = Time.time + tempoSumirAtaque;

            timeA = Time.time + timezinA;

            if (movimento > 0)
            {
                Ataque.GetComponent<Transform>().position = new Vector3(Player.GetComponent<Transform>().position.x + 0.408f, Player.GetComponent<Transform>().position.y + 0.044f);
                Ataque.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (movimento < 0)
            {
                Ataque.GetComponent<Transform>().position = new Vector3( Player.GetComponent<Transform>().position.x - 0.408f, Player.GetComponent<Transform>().position.y + 0.044f);
                Ataque.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (movimento == 0)
            {
                Ataque.SetActive(false);
            }            
        }

        if (Time.time < timeA)
        {
                attackCan = false;
        }
        else
        {
            attackCan = true;
        }

        if (Time.time > tempoAtivo)
        {
            Ataque.SetActive(false);
        }

        if (vida <= 0)
        {
            GetComponent<SpriteRenderer>().material.color = Color.black;
            canvas.SetActive(false);
            rb.velocity = new Vector2(0, 0);
            movimento = 0;
            GetComponent<SpriteRenderer>().flipY = true;
            CancelInvoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            noChão = 2;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            vida--;
            barraRage.fillAmount += 0.2f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            noChão = 1;
        }
    }
    void heal()
    {
        vida++;
    }
}
