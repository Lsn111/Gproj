using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tMovements : MonoBehaviour {



    //cambiando posiciones de zaphira y turmalina
    Vector3 tempPos;

    //variables de movimientos
    public float xSpeed = 0.1f;
    public float movX;
    public float currentPosition;
    public float inputX;

    //variables de salto
    public float jumpingForce = 350f;
    public Transform foot;
    public float footRadius = 0.05f;
    public LayerMask floor;
    public bool onFloor;

    //variables de ataque
    private bool attacking1 = false;

    private float attackTimer = 0;
    private float attackCd = 0.3f;

    public Collider2D attackTrigger;


    //animations

    public GameObject turmalina;
    Animator animatorTurmalina;

    private void Awake()
    {
        animatorTurmalina = GetComponent<Animator>();
        attackTrigger.enabled = false;
    }


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //moviendo en x
        float inputX = Input.GetAxis("Horizontal"); //movimiento en eje x

        //Debug.Log(inputX);

        if (inputX > 0) {
            movX = transform.position.x + (inputX * xSpeed);
            transform.position = new Vector3(movX, transform.position.y, 0);
            transform.localScale = new Vector3(1, 1, 1);
            movX = currentPosition;
        };
        if (inputX < 0) {
            movX = transform.position.x + (inputX * xSpeed);
            transform.position = new Vector3(movX, transform.position.y, 0);
            transform.localScale = new Vector3(-1, 1, 1);
            movX = currentPosition;
        };

        if (inputX != 0 && onFloor)
        {
            animatorTurmalina.SetFloat("xSpeed", 1);
        }
        else {
            animatorTurmalina.SetFloat("xSpeed", 0);
        }

        Debug.Log("xSpeed: " + xSpeed);
        //saltando

        

        onFloor = Physics2D.OverlapCircle(foot.position, footRadius, floor);
        //Debug.Log(onFloor);
        if (onFloor)
        {
            animatorTurmalina.SetBool("onFloor", true);
        }
        else {
            animatorTurmalina.SetBool("onFloor", false);
        }


        if (onFloor && Input.GetKeyDown(KeyCode.Space)) {
            GetComponent<Rigidbody2D>().AddForce (new Vector2(0,jumpingForce));
            animatorTurmalina.SetBool("onFloor", false);
        }

         if (onFloor && (Mathf.Abs(inputX) <= 0.1) && Input.GetKeyDown(KeyCode.M)) {
            var zaphira = GameObject.Find("zaphira");
            tempPos = transform.position;
            transform.position = zaphira.transform.position;
            zaphira.transform.position = tempPos;
        }
        
        //atacando
    
        if (Input.GetKeyDown(KeyCode.L) && !attacking1){

            attacking1 = true;
            attackTimer = attackCd;

            attackTrigger.enabled = true;
        }

        if (attacking1){
            if (attackTimer > 0){
                attackTimer -= (Time.deltaTime/2);
            } else {
                attacking1 = false;
                attackTrigger.enabled = false;
            }
        } 

        animatorTurmalina.SetBool("attacking1", attacking1);
    }
}
