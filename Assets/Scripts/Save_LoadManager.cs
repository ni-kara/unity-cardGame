
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

public class Save_LoadManager : MonoBehaviour
{
    private Deck deck;

    // Start is called before the first frame update
    void Start()
    {   
        Load(1);
        Load(2);
        Load(3);          
    }

    // Update is called once per frame
    void Update()
    {

    }

   public void Save(int numDeck)
   {
        List<Deck> listDeck = new List<Deck>();
        listDeck = Load(numDeck);
                
        if (listDeck.Count < 50)
        {                
            if (GameObject.Find("view-card").GetComponent<Monster>().Id != "")
            {
                Deck d = new Deck();
                d.Id = GameObject.Find("view-card").GetComponent<Monster>().Id;
                d.Page = GameObject.Find("view-card").GetComponent<Monster>().NumPage;

                listDeck.Add(d);
            }

            GameObject.Find("Btn-SaveDeck-" + numDeck).GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = listDeck.Count + "/50";
            var deck = new { deck = listDeck };

            PlayerPrefs.SetString("deck_"+numDeck,JsonConvert.SerializeObject(deck));
        }
   }

    private List<Deck> Load(int numDeck)
    {
        List<Deck> listDeck = new List<Deck>();
       
        string readData = PlayerPrefs.GetString("deck_"+numDeck);
        if (readData != "" && readData!=null)
        {
            foreach (var item in Newtonsoft.Json.Linq.JToken.Parse(readData).SelectToken("deck"))
            {
                deck = new Deck();
                deck.Id = item.SelectToken("Id").ToString();
                deck.Page = int.Parse(item.SelectToken("Page").ToString());

                listDeck.Add(deck);
            }
        }
          GameObject.Find("Btn-SaveDeck-" + numDeck).GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = listDeck.Count + "/50"; 

       return listDeck;
   }
}

public class Deck 
{
    private string id;
    private int page;
    public Deck() 
    {
        
    }
    public string Id { get => id; set => id = value; }
    public int Page { get => page; set => page = value; }
}
