using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jump_force = 100f;

    public bool can_move = true;

    private Rigidbody2D my_rb;
    private Vector2 my_pos;
    private bool is_grounded = true;

    void Start()
    {
        my_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jumping();
    }


    private void Movement()
    {
        if (can_move)
        {
            float direction_x = Input.GetAxis("Horizontal");

            my_pos = new Vector2(direction_x, 0f).normalized * speed;
            my_rb.velocity = my_pos;
        }
    }


    private void Jumping()
    {
        if (Input.GetKey(KeyCode.Space) && is_grounded)
        {
            my_pos.y = jump_force / 2;
            my_rb.velocity = my_pos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Chão"))
        {
            is_grounded = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chão"))
        {
            is_grounded = false;
        }
    }
}
