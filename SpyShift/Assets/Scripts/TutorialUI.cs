using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialUI;
    public GameObject tutorialBtn;
    public Button menuBtn;

    public void ShowOrHideTutorialUI()
    {
        tutorialUI.SetActive(!tutorialUI.activeSelf);
        tutorialBtn.SetActive(!tutorialBtn.activeSelf);
        Time.timeScale = tutorialUI.activeSelf ? 0 : 1;
    }

    void Start()
    {
        menuBtn.onClick.AddListener(Menu);
    }

    void Menu()
    {
        //Debug.Log("Menu Screen.");
        SceneManager.LoadScene("MenuScene");
    }
}
