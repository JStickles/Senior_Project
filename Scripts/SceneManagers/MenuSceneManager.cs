using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public Button PlayButton;
    public Button InstructionButton;
    public Button CreditButton;
    public Button NewGameButton;

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener(OnPlyaerPlayButtonClick);
        CreditButton.onClick.AddListener(OnPlyaerCreditButtonClick);
        InstructionButton.onClick.AddListener(OnPlyaerInstructionButtonClick);
        NewGameButton.onClick.AddListener(OnPlyaerNewGameButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlyaerPlayButtonClick()
    {
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(2);
    }

    void OnPlyaerInstructionButtonClick()
    {
        StartCoroutine(LoadInstructionScene());
    }

    IEnumerator LoadInstructionScene()
    {
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(5);
    }

    void OnPlyaerCreditButtonClick()
    {
        StartCoroutine(LoadCreditScene());
    }

    IEnumerator LoadCreditScene()
    {
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(4);
    }

    void OnPlyaerNewGameButtonClick()
    {
        PlayerPrefs.SetFloat("xCoord", 374.4f);
        PlayerPrefs.SetFloat("zCoord", 388.4f);
        PlayerPrefs.SetInt("bosses", 0);
        PlayerPrefs.SetInt("enemy", 0);
        StartCoroutine(LoadMainScene());
    }
}
