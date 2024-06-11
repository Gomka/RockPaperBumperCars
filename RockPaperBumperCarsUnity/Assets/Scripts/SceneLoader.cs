using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadTargetScene(int target) {
        SceneManager.LoadScene(target);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
