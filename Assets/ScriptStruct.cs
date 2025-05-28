using UnityEngine;

[System.Serializable]
public class ScriptStruct
{
    public string topic;
    public Line[] script;
}

[System.Serializable]
public class Line
{
    public string character;
    public string text;
}

