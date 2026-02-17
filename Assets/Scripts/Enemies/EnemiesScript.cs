using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Enemie Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private int life = 15;
    [SerializeField] private int damage = 5;

    private Animator anim_ref;
    
    public bool can_attack = false;


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

    private void Idle()
    {
        anim_ref.Play("EnemieIdle");
        can_attack = false;

        //Giving the MathControllerParams
        var combat_ref = FindObjectOfType<CombatSystem>();

        //Telling that math_system returns
        combat_ref.can_math = false;
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


        if (life <= 0)
        {
            
            Destroy(gameObject);
        }
    }

    private void DamageEnemie()
    {
        var player_ref = FindObjectOfType<PlayerController>();
        player_ref.HitDamage(damage);
    }
}

