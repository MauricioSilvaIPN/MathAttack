using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int life = 15;
    [SerializeField] private int damage = 5;
    [SerializeField] public int level = 1;

    [Header("UI Components")]
    [SerializeField] private Text level_label;
    [SerializeField] private Image exp_bar;
    [SerializeField] private Image life_bar;
    
    private Animator anim_ref;
    private MathSystem math_ref;

    private int experience = 0;
    private int max_experience = 20;
    private int max_life = 15;

    public bool can_attack = false;
    public bool enemie_death = false;

    // Start is called before the first frame update
    void Start()
    {
        life = max_life;

        anim_ref = GetComponent<Animator>();
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;

        math_ref = FindAnyObjectByType<MathSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Attacking();
        LevelUp();

        life_bar.fillAmount = ((float) life / (float) max_life);
    }


    private void LevelUp()
    {
        //Increase player level
        level_label.text = level.ToString();

        if (experience >= max_experience)
        {
            level++;
            max_experience *= 2;
            max_life *= 2;
            life = max_life;

            exp_bar.fillAmount = 0;
        }

    }


    private void Attacking()
    {
        //Attack just when player_attack is true
        if (can_attack)
        {
            anim_ref.Play("Attacking");
        }
    }


    private void Idle() //Animation Event
    {
        //Giving the Player params
        can_attack = false;
        anim_ref.Play("Idle");

        //Giving the MathControllerParams
        var combat_ref = FindObjectOfType<CombatSystem>();

        //Generating CombatSystem
        if (combat_ref.can_enemie == false && enemie_death == false) 
        { 
            combat_ref.CombatStart();
        }
    }

    private void DamageEnemie() //Animation Event
    {
        var enemie_ref = FindObjectOfType<EnemiesScript>();
        enemie_ref.HitDamage(damage);
    }


    public void HitDamage(int amount)
    {
        life -= amount;

        if (life <= 0)
        {
            var enemie_ref = FindObjectOfType<EnemiesScript>();
            enemie_ref.player_death = true;
        }


    }

    public void AddingEXP(int amount)
    {
        experience += amount;
        exp_bar.fillAmount = ((float) experience / (float) max_experience);
    }

}
