using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Enemie Stats")]
    [SerializeField] private int life = 15;
    [SerializeField] private int max_life = 15;
    [SerializeField] private int damage = 5;
    [SerializeField] private int exp_gained = 5;

    [Header("UI Components")]
    [SerializeField] private Image life_bar;
    [SerializeField] private Canvas my_canva;
    [SerializeField] private GameObject ui_stats;

    private Animator anim_ref;

    public bool can_attack = false;
    public bool player_death = false;


    // Start is called before the first frame update
    void Start()
    {
        anim_ref = GetComponent<Animator>();

        GetComponentInChildren<Canvas>().worldCamera = Camera.main;

        max_life = life;

    }

    // Update is called once per frame
    void Update()
    {
        EnemieAttack();

        life_bar.fillAmount = ((float) life / (float) max_life);
    }

    private void GenerateMathSystem() //Animation Event
    {
        anim_ref.Play("EnemieIdle");
        can_attack = false;
        
        SettingAlpha(true, 1f);

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


    private void StayIdle()
    {
        SettingAlpha(true, 1f);
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
        anim_ref.Play("EnemieHurt");

        life -= amount;

        EnemieDeath();
    }


    public void SettingAlpha(bool checking, float alpha)
    {
        ui_stats.GetComponent<Renderer>().enabled = checking;

        Transform parentTransform = my_canva.transform;
        foreach (Transform child in parentTransform)
        {
            CanvasRenderer canva_sprite = child.GetComponent<CanvasRenderer>();
            if (canva_sprite != null)
            {
                canva_sprite.SetAlpha(alpha);
            }
        }
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

