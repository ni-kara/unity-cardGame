using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropHandler : MonoBehaviour
{
    private Monster monster;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        monster = collision.GetComponent<Monster>().GetMonster();
       
        GameObject.Find("Content").GetComponent<RetieveCardsData>().CallGetRequestImage(monster.ImageUrlHiRes, gameObject.GetComponent<Image>());
        Destroy(collision);
        Destroy(collision.gameObject,1f);
    }
}
