using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneTransitions : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeTo(int sceneIndex)
    {
        StartCoroutine(Fade(sceneIndex));
    }

    IEnumerator Fade(int sceneIndex)
    {
        animator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneIndex);
    }

    public void Reset()
    {
        FadeTo(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
