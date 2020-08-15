using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    int playerCnt;


    //int currentIndex;
    void Start()
    {
        //currentIndex = SceneManager.GetActiveScene().buildIndex;

        Button[] MainMenu = FindObjectsOfType<Button>();
        foreach (Button button in MainMenu)
        {
            if (button.tag == "MainMenu")
            {
                button.onClick.AddListener(onMenuClick);
            }
        }
    }


    public void onMenuClick()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.CompareTag("Respawn") && other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = other.GetComponent<PlayerMovement>().lastCheckPoint;
            other.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }

        if (this.gameObject.CompareTag("Quit") && other.gameObject.CompareTag("Player"))
        {
            playerCnt++;
            if(playerCnt == 2) // later i can update it for 4 player
            {
                playerCnt = 0;
                Invoke("transitionToQuitMenu", 8f);
                
            }
        }
    }

    public void onTutorialClick()
    {
        SceneManager.LoadScene("Test");
    }

    /* public void onStartGameClick()
     {
         SceneManager.LoadScene(currentIndex + 1);
     }*/

    public void onPlayAgainClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    private void transitionToQuitMenu()
    {
        SceneManager.LoadScene("QuitMenu");
    }
}
