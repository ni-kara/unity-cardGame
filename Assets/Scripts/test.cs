using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    string aaa;
    cl c = new cl();
    // Start is called before the first frame update
    void Start()
    {
        if (c.a !="")
        {
            print("aaa");
        }
        if (c.a== null)
        {
            print("bbb");
        }
        JToken.FromObject("aaa");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class cl {
    public string a="";
}
