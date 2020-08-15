using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Image img;

    private Vector3 imgScal;
    private int gameSceneIndex = 3;

    // Start is called before the first frame update
    void Start()
    {
        imgScal = img.transform.localScale;
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(gameSceneIndex);
        while(gameLevel.progress < 1)
        {
            imgScal.x = gameLevel.progress;
            img.transform.localScale = imgScal;
            yield return new WaitForEndOfFrame();
        }
    }
}
