using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject record_ref;

    [Header("Player Stats")]
    [SerializeField] private int life = 15;
    [SerializeField] private int damage = 5;
    [SerializeField] private int level = 1;

    [Header("UI Components")]
    [SerializeField] private Canvas my_canva;
    [SerializeField] private GameObject ui_stats;
    [SerializeField] private Text level_label;
    [SerializeField] private Image exp_bar;
    [SerializeField] private Image life_bar;

    [Header("Sounds")]
    [SerializeField] private AudioClip death_fx;
    [SerializeField] private AudioClip levelup_fx;
    [SerializeField] private AudioClip damage_fx;

    private Animator anim_ref;

    private int experience = 0;
    private int max_experience = 20;
    private int max_life = 15;

    public bool enemie_death = false;

    // Start is called before the first frame update
    void Start()
    {
        life = max_life;

        anim_ref = GetComponent<Animator>();
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        LevelUp();

        life_bar.fillAmount = ((float) life / (float) max_life);
    }


    private void LevelUp()
    {
        //Increase player level
        level_label.text = level.ToString();

        if (experience >= max_experience)
        {
            AudioSource.PlayClipAtPoint(levelup_fx, new Vector3(0f, 0f, -10f));

            level++;
            max_experience *= 2;
            max_life *= 2;
            life = max_life;

            exp_bar.fillAmount = 0;
        }

    }


    private void GenerateMathSystem() //Animation Event
    {
        //Giving the Player params
        anim_ref.SetBool("Can_Attack", false);

        //Visible Canva
        SettingAlpha(true, 1f);


        //Giving the MathControllerParams
        var combat_ref = FindObjectOfType<CombatSystem>();

        //Generating CombatSystem
        if (combat_ref.can_enemie == false && enemie_death == false) 
        { 
            combat_ref.CombatStart();
        }
    }


    private void StayIdle()
    {
        //Visible Canva
        SettingAlpha(true, 1f);
    }



    private void DamageEnemie() //Animation Event
    {
        var enemie_ref = FindObjectOfType<EnemiesScript>();
        enemie_ref.HurtDamage(damage);
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


    public void HurtDamage(int amount)
    {
        life -= amount;

        anim_ref.Play("Hurt");

        AudioSource.PlayClipAtPoint(damage_fx, new Vector3(0f, 0f, -10f));

        if (life <= 0)
        {
            AudioSource.PlayClipAtPoint(death_fx, new Vector3(0f, 0f, -10f));

            var enemie_ref = FindObjectOfType<EnemiesScript>();
            enemie_ref.player_death = true;

            Instantiate(record_ref, Vector3.zero, Quaternion.identity);
        }


    }

    public void AddingEXP(int amount)
    {
        experience += amount;
        exp_bar.fillAmount = ((float) experience / (float) max_experience);
    }

}
