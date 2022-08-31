using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrelloCard 
{ 
    public string name = "";
    public string desc = "";
    public string due = "null";
    public string idList = "";

    public TrelloCard(string name, string desc, string due, string idList)
    {
        this.name = name;
        this.desc = desc;
        this.due = due;
        this.idList = idList;
    }
}
