using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rockUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI leaveText;
    private bool leaveMenu;

    private void Start()
    {
        leaveText.gameObject.SetActive(false);
        leaveMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
        {
            leaveText.gameObject.SetActive(true);
            leaveMenu = true;
        } 
      
      if(leaveMenu == true)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                leaveText.gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene("ForestMazeMap");
            }
        }
    }
}
