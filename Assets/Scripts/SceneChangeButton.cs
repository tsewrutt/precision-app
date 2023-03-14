using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    public Button levelchoice_b;
   // public Animator transition;

    void Start()
    {
        levelchoice_b.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        // StartCouroutine(startransition);
        SceneManager.LoadScene("settings");
    }
    /*IEnumerator startransition()
    {
        transition.SetTrigger("exit");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("level1");

    }*/
}
