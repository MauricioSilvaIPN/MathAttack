using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text points_text;
    [SerializeField] private Text correctq_text;
    [SerializeField] private Text incorrectq_text;
    [SerializeField] private Button back_btn;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        back_btn.onClick.AddListener(MenuBack);

        ShowGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowGameOver()
    {
        var generator_ref = FindObjectOfType<CombatSystem>();

        generator_ref.ShowRecord(points_text, correctq_text, incorrectq_text);
    }


    private void MenuBack()
    {
        SceneManager.LoadScene(0);
    }
}
