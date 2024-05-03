using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public Button playButton, howButton, quitButton, exitButton, tutorialButton;
    public GameObject instructions;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(Play);
        howButton.onClick.AddListener(HowTo);
        tutorialButton.onClick.AddListener(Tutorial);
        quitButton.onClick.AddListener(Quit);
        exitButton.onClick.AddListener(ExitInstructions);

    }


    void Play()
    {
        //Debug.Log("Play!");
        SceneManager.LoadScene(0);
    }

    void Tutorial()
    {
        //Debug.Log("Tutorial.");
        SceneManager.LoadScene("TutorialScene");
    }

    void HowTo()
    {
        //Debug.Log("Instructions.");

        if (instructions != null)
        {
            instructions.SetActive(true);
        }
    }

    void ExitInstructions()
    {
        if (instructions)
        {
            //Debug.Log("Exit");
            instructions.SetActive(false);
        }
    }

    void Quit()
    {
        //Debug.Log("Quit.");
        Application.Quit();
    }
}
