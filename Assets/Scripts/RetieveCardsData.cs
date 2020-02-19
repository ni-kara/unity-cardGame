using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RetieveCardsData : MonoBehaviour
{

    public GameObject panelCards;
    
    private string urlLink = "https://api.pokemontcg.io/v1/cards?page=";
    
    private Monster monster;
    private List<Monster> listMonster = new List<Monster>();

    private bool finishRetrieveData = false;
    private bool finishCreatePanelCard = false;

    private int numPage = 1;
    private int countCard = 0;

    private float webRequestImageProgress = 0;
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //print(webRequestImageProgress);
        if (finishRetrieveData && webRequestImageProgress >= listMonster.Count)
        {
            LoadingEffect(false);
        }
        else
        {
            LoadingEffect(true);
        }
    }

    private void LoadingEffect(bool enable) 
    {
        float fadeOut = GameObject.Find("FadeOut").GetComponent<Image>().color.a;
        if (!enable)
        {
            if (fadeOut >= 0)
                fadeOut -= Time.smoothDeltaTime;

            if (fadeOut <0.1f)
            {
                GameObject.Find("FadeOut").GetComponent<Image>().enabled = false;
                GameObject.Find("settingIcon").GetComponent<Image>().enabled =false;
            }
        }
        else
        {
            if (fadeOut <= 1)
                fadeOut += Time.smoothDeltaTime;

            if (fadeOut > 0.1f)
            {
                GameObject.Find("FadeOut").GetComponent<Image>().enabled = true;
                GameObject.Find("settingIcon").GetComponent<Image>().enabled = true;
            }
        }

        GameObject.Find("FadeOut").GetComponent<Image>().color = new Color(0, 0, 0, fadeOut);
        GameObject.Find("settingIcon").GetComponent<Image>().color = new Color(1, 1, 1, fadeOut);
    }

    public void CallManagerFunction(int numPage)
    {
        this.numPage = numPage;
        StartCoroutine(ManagerFunction(numPage));
    }

    IEnumerator ManagerFunction(int numPage) 
    {
        listMonster.Clear();
        countCard = 0;
        webRequestImageProgress = 0;
        finishRetrieveData = false;
        finishCreatePanelCard = false;
       
        StartCoroutine(GetRequest(urlLink + numPage));

        yield return new WaitUntil(() => this.finishRetrieveData);

        StartCoroutine(CreatePanelCards());
       
        yield return new WaitUntil(() => this.finishCreatePanelCard);

        StartCoroutine(LoadImages());
    }

    public IEnumerator CreatePanelCards() 
    {
        yield return null;

        int numPanel = listMonster.Count/5;

        if (listMonster.Count % 5 != 0)
            numPanel++;

        for (int i = 0; i < numPanel; i++)
        {
            Destroy(GameObject.Find("PanelCards-" + (i+1)));
        }

        for (int i = 0; i < numPanel; i++)
        {
            GameObject panelCard = Instantiate(panelCards, panelCards.gameObject.transform.position, Quaternion.identity, gameObject.transform);
            panelCard.name = "PanelCards-" + (i+1);
        }

        if (GameObject.Find("PanelCards-"+(numPanel)))
        {
            finishCreatePanelCard = true;
        }
    }

    public IEnumerator LoadImages() 
    {
        yield return null;

        Image image;
        foreach (var item in listMonster)
        {
            image = GetComponent<Image>();
            image = GameObject.Find("PanelCards-" + ((countCard / 5) + 1)).GetComponent<Transform>().GetChild(countCard % 5).GetComponent<Image>();
            
            StartCoroutine(GetRequestImage(item.ImageUrl, image));
           
            image.GetComponent<Monster>().SetMonster(item);
            
            countCard++;
        }  
    }

    public IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {            
            // Request and wait for the desired page.
             yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
               var data = Newtonsoft.Json.Linq.JToken.Parse(webRequest.downloadHandler.text).SelectToken("cards");  //Newtonsoft.Json.Linq.JToken.Parse(webRequest.downloadHandler.text).SelectToken("cards")[0].SelectToken("imageUrl");
                
                foreach (var item in data)
                {
                     if (item.SelectToken("supertype").ToString() == "Pokémon")
                     {
                        monster = new Monster();

                        monster.Id = item.SelectToken("id").ToString();
                        monster.Name = item.SelectToken("name").ToString();
                        if (item.SelectToken("subtype").ToString() != "LEGEND")
                            monster.Hp = int.Parse(item.SelectToken("hp").ToString());
                        monster.ImageUrl = item.SelectToken("imageUrl").ToString();
                        monster.ImageUrlHiRes = item.SelectToken("imageUrlHiRes").ToString();
                        monster.Type =item.SelectToken("types")[0].ToString();
                        JToken token = item.SelectToken("rarity"); //There is pokemon card without rarity
                        if (token != null) 
                            monster.Ratity = item.SelectToken("rarity").ToString();
                         monster.NumPage = numPage;

                         listMonster.Add(monster);                      
                     }
                }
            }
            finishRetrieveData = true;      
        }    
    }
   
   
    public void CallGetRequestImage(string uri, Image templateCard) {
        StartCoroutine(GetRequestImage(uri, templateCard));
    }

    public IEnumerator GetRequestImage(string uri, Image templateCard)
    {
         using (UnityWebRequest webRequestImage = UnityWebRequestTexture.GetTexture(uri))
        {
           // Request and wait for the desired page.
            yield return webRequestImage.SendWebRequest();

            webRequestImageProgress += webRequestImage.downloadProgress;
             
            if (webRequestImage.isNetworkError || webRequestImage.isHttpError)
            {
                Debug.Log("uri: "+uri+" : Error: " + webRequestImage.error);
            }
            else
            {
                Texture2D tex2D = DownloadHandlerTexture.GetContent(webRequestImage);
             
                Sprite image = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0, 0));
                
                templateCard.sprite = image;
            }
         }
    }

    public void SortByHp() 
    {
        listMonster.Sort((m1, m2) => m2.Hp.CompareTo(m1.Hp));
        countCard = 0;
        webRequestImageProgress = 0;
        StartCoroutine(LoadImages());
    }
    public void SortByType()
    {
        listMonster.Sort((m1, m2) => m2.Type.CompareTo(m1.Type));
        countCard = 0;
        webRequestImageProgress = 0;
        StartCoroutine(LoadImages());
    }
    public void SortByRarity() 
    {
        listMonster.Sort((m1, m2) => m2.Ratity.CompareTo(m1.Ratity));
        countCard = 0;
        webRequestImageProgress = 0;
        StartCoroutine(LoadImages());
    }
}

