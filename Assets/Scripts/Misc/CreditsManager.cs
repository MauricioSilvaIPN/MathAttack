using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private Button back_button;

    // Start is called before the first frame update
    void Start()
    {
        back_button.onClick.AddListener(BackMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
}
