using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject gamename;
    public GameObject start;
    public GameObject exit;
    public GameObject creditsbutton;
    public GameObject backCredits;
    public GameObject tutorialbutton;
    public GameObject backTutorial;
    public GameObject Credit;
    public GameObject Tutor;
    private SceneManager sc;
    AsyncOperation asyncOperation;
    // Use this for initialization
    void Start()
    {
        asyncOperation = SceneManager.LoadSceneAsync("GameScene");
        asyncOperation.allowSceneActivation = false;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
        start.GetComponent<Button>().onClick.AddListener(LaunchGame);
        exit.GetComponent<Button>().onClick.AddListener(Quit);
        creditsbutton.GetComponent<Button>().onClick.AddListener(ShowCredits);
        backCredits.GetComponent<Button>().onClick.AddListener(CloseCredits);
        tutorialbutton.GetComponent<Button>().onClick.AddListener(ShowTutorial);
        backTutorial.GetComponent<Button>().onClick.AddListener(CloseTutorial);
    }
    void ShowCredits()
    {
        gamename.SetActive(false);
        Credit.SetActive(true);
        backCredits.SetActive(true);
        start.SetActive(false);
        exit.SetActive(false);
        creditsbutton.SetActive(false);
        tutorialbutton.SetActive(false);
    }
    void CloseCredits()
    {
        gamename.SetActive(true);
        Credit.SetActive(false);
        backCredits.SetActive(false);
        start.SetActive(true);
        exit.SetActive(true);
        creditsbutton.SetActive(true);
        tutorialbutton.SetActive(true);
    }
    void ShowTutorial()
    {
        gamename.SetActive(false);
        Tutor.SetActive(true);
        backTutorial.SetActive(true);
        start.SetActive(false);
        exit.SetActive(false);
        creditsbutton.SetActive(false);
        tutorialbutton.SetActive(false);
    }
    void CloseTutorial()
    {
        gamename.SetActive(true);
        Tutor.SetActive(false);
        backTutorial.SetActive(false);
        start.SetActive(true);
        exit.SetActive(true);
        creditsbutton.SetActive(true);
        tutorialbutton.SetActive(true);
    }
    void Quit()
    {
        //Debug.Log("exit pressed");
        Application.Quit();
    }

    void LaunchGame()
    {
        asyncOperation.allowSceneActivation = true;
    }
    void Update()
    {

    }
}
