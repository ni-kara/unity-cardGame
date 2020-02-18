using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadICardmage : MonoBehaviour
{
    private bool onceTime = false;
    private float timeOut = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("FadeOut").GetComponent<Image>().enabled)
        {
            if (gameObject.GetComponent<Monster>().Id != "" && gameObject.GetComponent<Button>().image.sprite.name == "UISprite" && !onceTime)
            {
                LoadImage();
                onceTime = true;
            }

            timeOut += Time.deltaTime;
            if (onceTime && timeOut >= 5f)
            {
                onceTime = false;
                timeOut = 0;
            }
        }
    }

    public void LoadImage() 
    {
        GameObject.Find("Content").GetComponent<RetieveCardsData>().CallGetRequestImage(gameObject.GetComponent<Monster>().ImageUrl, gameObject.GetComponent<Button>().image);
    }

}
