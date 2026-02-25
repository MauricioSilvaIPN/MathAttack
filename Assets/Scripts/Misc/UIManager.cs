using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button play_button;
    [SerializeField] private Button credits_button;
    [SerializeField] private Button quit_button;

    // Start is called before the first frame update
    void Start()
    {
        play_button.onClick.AddListener(PlayGame);
        credits_button.onClick.AddListener(CreditsUI);
        quit_button.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    private void CreditsUI()
    {
        SceneManager.LoadScene(1);
    }

    private void ExitGame() 
    { 
        Application.Quit();
    }

}
