using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditSceneManager : MonoBehaviour
{
    public Button clickButton;

    // Start is called before the first frame update
    void Start()
    {
        clickButton.onClick.AddListener(OnPlyaerClickButtonClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPlyaerClickButtonClick()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(1);
    }
}
