using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewCard : MonoBehaviour
{
    private Image imageView;
    private Monster monsterCard;
    // Start is called before the first frame update
    void Start()
    {
        imageView =GameObject.Find("view-card").GetComponent<Image>();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewCardMonster() 
    {
        monsterCard = gameObject.GetComponent<Monster>().GetMonster();
        if (monsterCard.Id != "")
        {
            print("click view card " + monsterCard.Id);
            GameObject.Find("Content").GetComponent<RetieveCardsData>().CallGetRequestImage(monsterCard.ImageUrlHiRes, imageView);
            GameObject.Find("view-card").GetComponent<Monster>().SetMonster(monsterCard.GetMonster());
        }
    }
}
