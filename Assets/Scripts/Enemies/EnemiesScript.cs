using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Enemie Stats")]
    [SerializeField] private int life = 15;
    [SerializeField] private int damage = 5;
    [SerializeField] private int exp_gained = 5;

    private Animator anim_ref;
    
    public bool can_attack = false;
    public bool player_death = false;


    // Start is called before the first frame update
    void Start()
    {
        anim_ref = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        EnemieAttack();
    }

    private void Idle() //Animation Event
    {
        anim_ref.Play("EnemieIdle");
        can_attack = false;

        //Giving the MathControllerParams
        var combat_ref = FindObjectOfType<CombatSystem>();

        if (player_death == false) 
        {
            combat_ref.CombatStart();
        }

        //Giving the PlayerController params

        var player_ref = FindObjectOfType<PlayerController>();

        player_ref.enemie_death = false;
    }


    public void EnemieAttack()
    {
        if (can_attack)
        {
            anim_ref.Play("EnemieAttack");
        }
    }


    public void HitDamage(int amount)
    {
        life -= amount;

        EnemieDeath();
    }


    public void EnemieDeath()
    {
        if (life <= 0)
        {
            //Destroy enemie
            Destroy(gameObject);

            //Gaining EXP and telling to player that enemie is death
            var player_ref = FindObjectOfType<PlayerController>();
            exp_gained = Random.Range(5, 10);

            player_ref.AddingEXP(exp_gained);
            player_ref.enemie_death = true;

            //Disabled math operation system
            var combat_ref = FindObjectOfType<CombatSystem>();

            combat_ref.can_enemie = true;
        }
    }


    private void DamageEnemie() //Animation Event
    {
        var player_ref = FindObjectOfType<PlayerController>();
        player_ref.HitDamage(damage);
    }
}

