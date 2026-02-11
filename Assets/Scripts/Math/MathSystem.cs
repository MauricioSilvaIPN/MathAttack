using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MathSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private string[] operators = {"+", "-", "/", "*"};
    private int result;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Playable"))
        {
            MathOperation();
        }
    }


    private void MathOperation()
    {
        int random_number = Random.Range(0, 9);
        int random_operator = Random.Range(0, operators.Length);

        int number_one = random_number;
        int number_two = random_number;

        string current_operator = operators[random_operator];


        string text_show = string.Concat(number_one, current_operator, number_two);

        switch (current_operator)
        {
            case "+":
                {
                    result = number_one + number_two;
                    break;
                }
            case "-":
                {
                    result = number_one - number_two;
                    break;
                }
            case "/":
                {
                    result = number_one / number_two;
                    break;
                }
            case "*":
                {
                    result = number_one * number_two;
                    break;
                }
        }
        
    }
}
