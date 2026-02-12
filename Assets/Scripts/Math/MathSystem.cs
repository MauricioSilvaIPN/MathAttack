using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MathSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private string[] operators = {"+", "-", "/", "x"};
    private int result;
    private float math_timer = 10f;

    [Header("InputManagement")]
    [SerializeField] private Button button_ref;
    [SerializeField] private Text operation_text;
    [SerializeField] private InputField input_ref;

    void Start()
    {
        button_ref.onClick.AddListener(CheckResult);
        MathOperation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void MathOperation()
    {
        if (math_timer > 0f)
        {
            math_timer -= Time.deltaTime;

            //Creating variables of operations
            int random_number = Random.Range(0, 9);
            int random_operator = Random.Range(0, operators.Length);

            int number_one = random_number;
            int number_two = random_number;

            string current_operator = operators[random_operator];


            string text_show = string.Concat(number_one, current_operator, number_two);
            operation_text.text = text_show;

            //Detecting player to stop his movement
            var player_ref = FindObjectOfType<PlayerController>();

            player_ref.can_move = false;


            //Matching the correct operator
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
                case "x":
                    {
                        result = number_one * number_two;
                        break;
                    }
            }
        }
    }

    private void CheckResult()
    {
        if (input_ref.text == result.ToString())
        {
            var player_ref = FindObjectOfType<PlayerController>();
            var enemie_ref = FindObjectOfType<GoblinController>();

            player_ref.can_move = true;
            enemie_ref.EnemieDeath();
            Destroy(gameObject);
        }
    }
}


