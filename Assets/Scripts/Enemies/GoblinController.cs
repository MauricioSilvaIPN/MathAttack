using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoblinController : MonoBehaviour
{
    [Header("Enemie Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private int life = 1;

    [Header("States")]
    [SerializeField] private string current_state = "Patrol";

    [Header("MathOperatorReference")]
    [SerializeField] private GameObject math_system;

    private bool can_patrol = true;

    private Rigidbody2D my_rb;
    private Vector3 enemie_pos;

    // Start is called before the first frame update
    void Start()
    {
        my_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();

        switch (current_state)
        {
            case "Patrol":
                {
                    Patrolling();
                    break;
                }
            case "Death":
                {
                    EnemieDeath();
                    break;
                }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Playable"))
        {
            Instantiate(math_system, transform.position, Quaternion.identity);
        }

    }


    private void Patrolling()
    {
        enemie_pos.x = speed;
        my_rb.velocity = enemie_pos;
    }




    public void EnemieDeath()
    {

        Destroy(gameObject);

    }


    private void ChangeState() 
    {
        if (can_patrol)
        {
            current_state = "Patrol";
        }

        if (life <= 0)
        {
            can_patrol = false;
            current_state = "Death";
        }
    } 
}
