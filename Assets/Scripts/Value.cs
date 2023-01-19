using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class Value
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string path { get; set; }
    public Animations animations { get; set; }
    public string animationId { get; set; }
}