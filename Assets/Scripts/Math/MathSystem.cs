using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MathSystem : MonoBehaviour
{
    private string[] operators = {"+", "-", "/", "x"};
    private float result;
    private float math_timer = 10f;

    [Header("InputManagement")]
    [SerializeField] private Button button_ref;
    [SerializeField] private Text operation_text;
    [SerializeField] private InputField input_ref;

    private int math_level = 0;

    public int max_number = 10;

    // Start is called before the first frame update
    void Start()
    {
        button_ref.onClick.AddListener(CheckResult);
        MathOperation();

        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var player_ref = FindObjectOfType<PlayerController>();

        if (player_ref != null)
        {
            math_level = player_ref.level;
        }
    }


    private void MathOperation()
    {
        if (math_timer > 0f)
        {
            //math_timer -= Time.deltaTime;

            //Creating variables of operations
            int random_operator = Random.Range(0, operators.Length);

            int number_one = Random.Range(1, max_number);
            int number_two = Random.Range(1, max_number);

            string current_operator = operators[random_operator];

            string text_show = string.Concat(number_one, current_operator, number_two);
            operation_text.text = text_show;


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
        var player_ref = FindObjectOfType<PlayerController>();
        var enemie_ref = FindObjectOfType<EnemiesScript>();

        if (input_ref.text == result.ToString()) //If result is correct, player attack
        {
            Destroy(gameObject);

            player_ref.can_attack = true;
        }

        if (input_ref.text != result.ToString()) //If result is incorrect, enemie attack
        {
            Destroy(gameObject);

            enemie_ref.can_attack = true;
        }
    }
}


