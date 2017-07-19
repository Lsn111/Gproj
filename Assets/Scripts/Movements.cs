using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour {


    //movement variables
    public float xSpeed = 0.1f;
    public float movX;
    public float currentPosition;
    public float inputX;

    //jumping variables
    public float jumpingForce = 350f;
    public Transform foot;
    public float footRadius = 0.05f;
    public LayerMask floor;
    public bool onFloor;

    //animations

    Animator animator1;

    private void Awake()
    {
        animator1 = GetComponent<Animator>();
    }


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //moving in x
        float inputX = Input.GetAxis("Horizontal"); //Movement in x axis

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
            animator1.SetFloat("xSpeed", 1);
        }
        else {
            animator1.SetFloat("xSpeed", 0);
        }


        //jumping

        onFloor = Physics2D.OverlapCircle(foot.position, footRadius, floor);
        //Debug.Log(onFloor);
        if (onFloor)
        {
            animator1.SetBool("onFloor", true);
        }
        else {
            animator1.SetBool("onFloor", false);
        }


        if (onFloor && Input.GetKeyDown(KeyCode.Space)) {
            GetComponent<Rigidbody2D>().AddForce (new Vector2(0,jumpingForce));
            animator1.SetBool("onFloor", false);
        }



    }
}
