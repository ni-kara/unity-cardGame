using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HomeScene() {
        SceneManager.LoadScene("Home");
    }

    public void DeckBuilderScene() {
        SceneManager.LoadScene("DeckBuilder");
    }

    public void AboutScene() {
        SceneManager.LoadScene("About");
    }
}
