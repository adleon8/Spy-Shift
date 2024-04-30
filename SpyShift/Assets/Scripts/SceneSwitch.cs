using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public int sceneSwitchTo;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            //   SceneManager.LoadScene(sceneSwitchTo);
            GameObject.Find("Canvas").transform.Find("Winscreen").gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
