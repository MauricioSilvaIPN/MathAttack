using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MathSystem : MonoBehaviour
{
    [SerializeField] private float math_timer = 10f;

    private string[] operators = {"+", "-", "/", "x"};
    private float result;
    private float max_timer = 10f;

    private int experience = 5;
    private int max_number = 9;

    private CombatSystem manager_ref;

    [Header("InputManagement")]
    [SerializeField] private Button button_ref;
    [SerializeField] private Image timer_bar;
    [SerializeField] private Text operation_text;
    [SerializeField] private InputField input_ref;

    // Start is called before the first frame update
    void Start()
    {
        button_ref.onClick.AddListener(CheckResult);
        math_timer = max_timer;

        GetComponent<Canvas>().worldCamera = Camera.main;

        manager_ref = FindAnyObjectByType<CombatSystem>();

        IncreaseDifficult();
        MathOperation();
    }

    // Update is called once per frame
    void Update()
    {
        MathTimeOut();

        math_timer -= Time.deltaTime;

        timer_bar.fillAmount = ((float) math_timer / (float) max_timer);
    }


    private void MathOperation()
    {
        if (math_timer > 0f)
        {
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

    private void MathTimeOut()
    {
        var player_ref = FindAnyObjectByType<PlayerController>();
        var enemie_ref = FindAnyObjectByType<EnemiesScript>();

        if (math_timer <= 0)
        {
            //Adding incorrects answers points
            manager_ref.AddIncorrectAnswers(1);

            //Setting UI Enemie and Player off
            enemie_ref.SettingAlpha(false, 0f);
            player_ref.SettingAlpha(false, 0f);

            Destroy(gameObject);

            enemie_ref.GetComponent<Animator>().SetBool("Can_Attack", true);
        }
    }



    private void CheckResult()
    {
        var player_ref = FindObjectOfType<PlayerController>();
        var enemie_ref = FindObjectOfType<EnemiesScript>();
       

        if (input_ref.text == result.ToString()) //If result is correct, player attack
        {
            //Adding math experience and correct_answers points
            experience = Random.Range(5, 10);
            manager_ref.AddingMathEXP(experience);

            manager_ref.AddCorrectAnswers(1);

            //Destroy self
            Destroy(gameObject);

            //Player can attack
            player_ref.GetComponent<Animator>().SetBool("Can_Attack", true);

            //Setting UI Enemie and Player off
            enemie_ref.SettingAlpha(false, 0f);
            player_ref.SettingAlpha(false, 0f);
        }

        if (input_ref.text != result.ToString()) //If result is incorrect, enemie attack
        {
            //Adding incorrects answers points
            manager_ref.AddIncorrectAnswers(1);

            //Setting UI Enemie and Player off
            enemie_ref.SettingAlpha(false, 0f);
            player_ref.SettingAlpha(false, 0f);

            Destroy(gameObject);

            enemie_ref.GetComponent<Animator>().SetBool("Can_Attack", true);
        }
    }


    private void IncreaseDifficult()
    {
        if (manager_ref) 
        {
            if (manager_ref.math_level >= 5)
            {
                max_number *= manager_ref.math_level;
                operators = new string[] {"+", "-", "x"};
            }
        }
    }

}


