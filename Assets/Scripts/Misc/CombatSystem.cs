using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private PlayerController player_ref;
    [SerializeField] private GameObject math_system;
    [SerializeField] private GameObject[] enemies;

    public bool can_math = false;
    

    // Start is called before the first frame update
    void Start()
    {
        player_ref = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CombatStart();
    }


    private void CombatStart()
    {
        if (math_system != null && can_math == false) 
        {
            Instantiate(math_system, transform.position, Quaternion.identity);
            can_math = true;
        }
    }
}
