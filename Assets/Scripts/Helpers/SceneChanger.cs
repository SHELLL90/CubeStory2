using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneChanger : MonoBehaviour 
{
    public Text text;
    private void Start()
    {
        text.text = SceneManager.GetActiveScene().name;
    }
    public void ChangeScene(string name) 
    { 
        SceneManager.LoadScene(name);
    }
    public void Exit() 
    {
        Application.Quit(); 
    } 

}
