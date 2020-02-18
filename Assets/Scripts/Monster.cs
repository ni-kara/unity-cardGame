using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private string id = "";
    [SerializeField]
    private string name = "";
    [SerializeField]
    private string type = "";
    [SerializeField]
    private int hp = 0;
    [SerializeField]
    private string ratity = "";
    [SerializeField]
    private string imageUrl = "";
    [SerializeField]
    private string imageUrlHiRes = "";
    [SerializeField]
    private int numPage= 0;

    public Monster()
    {
    
    }

    public string Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Type { get => type; set => type = value; }
    public int Hp { get => hp; set => hp = value; }
    public string Ratity { get => ratity; set => ratity = value; }
    public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    public string ImageUrlHiRes { get => imageUrlHiRes; set => imageUrlHiRes = value; }
    public int NumPage { get => numPage; set => numPage = value; }

    public void SetMonster(Monster monster) {
        this.id = monster.id;
        this.name = monster.name;
        this.type = monster.type;
        this.hp = monster.hp;
        this.ratity = monster.ratity;
        this.imageUrl = monster.imageUrl;
        this.imageUrlHiRes = monster.imageUrlHiRes;
        this.numPage = monster.numPage;
    }
    public Monster GetMonster() {
        Monster monster = new Monster();
        monster.id = this.id;
        monster.name = this.name;
        monster.type = this.type;
        monster.hp = this.hp;
        monster.ratity = this.ratity;
        monster.imageUrl = this.imageUrl;
        monster.imageUrlHiRes = this.imageUrlHiRes;
        monster.numPage = this.numPage;

        return monster;
    }
}
