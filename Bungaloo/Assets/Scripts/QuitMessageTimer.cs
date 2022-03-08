using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMessageTimer : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("QuitMessageTime");
    }

    void Update(){
        if(Input.anyKey){
            SceneManager.LoadScene("Main Menu");
        }
    }

    IEnumerator QuitMessageTime(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Main Menu");
    }
}
