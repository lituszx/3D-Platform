using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool active;
    public void Play(int scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ActiveMenu (GameObject menu)
    {
        menu.SetActive(true);
    }
    public void Back(GameObject menu)
    {
        menu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (pauseMenu != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                active = !active;
                Time.timeScale = 0f;
            }
            pauseMenu.SetActive(active);
            if (!active == true)
            {
                Time.timeScale = 1f;
            }
        }
    }
}
