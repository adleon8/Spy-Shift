using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public GameObject infoPanel;
    public Button openInfoPanel;

    // Start is called before the first frame update
    void Start()
    {
        openInfoPanel.onClick.AddListener(ToggleInfoPanel);
        infoPanel.SetActive(false);
    }

    public void ToggleInfoPanel()
    {
        infoPanel.SetActive(!infoPanel.activeSelf);
    }
}
