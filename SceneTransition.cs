using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour{

    [SerializeField] float transitionTime = 1f;
    [SerializeField] GameObject buttonCanvas;
    Animator fadeAnimator;

    void Start(){

        fadeAnimator = GetComponent<Animator>();
        StartCoroutine(WaitForTransition());
    }

    public void LoadNextScene() {

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(SceneLoad(nextSceneIndex));
    }

    public void LoadPreviousScene(){

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(SceneLoad(nextSceneIndex));
    }

    public IEnumerator SceneLoad(int sceneIndex){

        fadeAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator WaitForTransition(){

        buttonCanvas.SetActive(false);
        yield return new WaitForSeconds(transitionTime);
        buttonCanvas.SetActive(true);
    }
}