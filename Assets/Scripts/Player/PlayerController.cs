using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int life = 15;
    [SerializeField] private int damage = 5;
    [SerializeField] public int level = 1;

    [SerializeField] private Text level_label;
    
    private Animator anim_ref;
    private MathSystem math_ref;

    private int experience = 0;
    private int max_experience = 20;

    public bool can_attack = false;
    public bool enemie_death = false;

    // Start is called before the first frame update
    void Start()
    {
        anim_ref = GetComponent<Animator>();
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;

        math_ref = FindAnyObjectByType<MathSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Attacking();
        LevelUp();
    }


    private void LevelUp()
    {
        //Increase player level
        level_label.text = level.ToString();

        if (experience >= max_experience)
        {
            level++;
            max_experience *= 2;

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
    }

}
