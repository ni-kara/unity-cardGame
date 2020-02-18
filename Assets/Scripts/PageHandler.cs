using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageHandler : MonoBehaviour
{
    private int numPage=1;
    // Start is called before the first frame update
    void Start()
    {
        drawNumPage();
        callPage(numPage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddNumPage() 
    {
        if (numPage < 100)
        {
            numPage++;
            drawNumPage();
            callPage(numPage);
        }
    }
    public void SubNumPage()
    {
        if (numPage> 1)
        {
            numPage--;
            drawNumPage();
            callPage(numPage);
        }
    }
    public int getNumPage() {
        return numPage;
    }

    private void drawNumPage() {
        transform.GetChild(2).GetComponent<Text>().text = numPage.ToString();   
    }    

    private void callPage(int numPage) {
        GameObject.Find("Content").GetComponent<RetieveCardsData>().CallManagerFunction(numPage);
    }
}
