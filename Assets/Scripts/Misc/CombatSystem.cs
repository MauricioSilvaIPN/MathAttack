using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSystem : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject math_system;
    [SerializeField] private GameObject[] enemies;

    [Header("Math Stats")]
    [SerializeField] public int math_level = 1;
    [SerializeField] private int math_exp = 0;
    private int max_exp = 20;

    [Header("Record Stats")]
    [SerializeField] private int points = 0;
    [SerializeField] private int correct_answers = 0;
    [SerializeField] private int incorrect_answers = 0;

    public bool can_enemie = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GenerateEnemies();
        CombatLevelUp();
    }


    public void CombatStart() //Instance Math Operation
    {
        Instantiate(math_system, transform.position, Quaternion.identity);
    }


    private void GenerateEnemies()
    {
        if (can_enemie) //If enemie is dead
        {
            int random_enemie = Random.Range(0, 3);
            var current_enemie = enemies[random_enemie];
            points += Random.Range(5, 10);

            Instantiate(current_enemie, transform.position, Quaternion.identity);

            can_enemie = false;
        }
    }


    private void CombatLevelUp()
    {
        if (math_exp > max_exp)
        {
            math_level++;
            max_exp *= 2;
        }
    }


    public void AddingMathEXP(int amount) //Add Math EXP
    {
        math_exp += amount;
    }


    public void AddCorrectAnswers(int amount) //Add Correct Answers
    {
        correct_answers += amount;
    }


    public void AddIncorrectAnswers(int amount) //Add Incorrect Answers
    {
        incorrect_answers += amount;
    }


    public void ShowRecord(Text text01, Text text02, Text text03)
    {
        text01.text = points.ToString();
        text02.text = correct_answers.ToString();
        text03.text = incorrect_answers.ToString();
    }
}
