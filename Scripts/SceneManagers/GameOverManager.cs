using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextMovement());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TextMovement()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}