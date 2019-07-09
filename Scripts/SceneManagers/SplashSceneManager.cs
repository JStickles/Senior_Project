using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashSceneManager : MonoBehaviour
{
    public Text splashText;
    
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
        for (int i = 0; i < 50; i++)
        {
            splashText.transform.position += new Vector3(14f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 15; i++)
        {
            splashText.transform.position += new Vector3((30 - i) * .5f , 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 18; i++)
        {
            splashText.transform.position -= new Vector3(4f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 50; i++)
        {
            splashText.transform.position += new Vector3(14f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 15; i++)
        {
            splashText.transform.position += new Vector3((i) * .5f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene(1);
    }
}
