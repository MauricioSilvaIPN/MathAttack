using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int life = 15;
    [SerializeField] private int damage = 5;

    [SerializeField] private CombatSystem manager_ref;
    
    private Animator anim_ref;

    public bool can_attack = false;

    void Start()
    {
        anim_ref = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attacking();
    }



    private void Attacking()
    {
        //Attack just when player_attack is true
        if (can_attack)
        {
            anim_ref.Play("Attacking");
        }
    }


    private void Idle()
    {
        //Giving the Player params
        can_attack = false;
        anim_ref.Play("Idle");

        //Giving the MathControllerParams
        var combat_ref = FindObjectOfType<CombatSystem>();

        //Telling that math_system returns
        combat_ref.can_math = false;
    }



    public void HitDamage(int amount)
    {
        life -= amount;

        if (life <= 0)
        {

        }


    }


    private void DamageEnemie()
    {
        var enemie_ref = FindObjectOfType<EnemiesScript>();
        enemie_ref.HitDamage(damage);
    }
}
