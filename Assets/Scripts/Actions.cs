using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class Actions
{
	public string description { get; set; }
	public string feedbackMessage { get; set; }
	public int actionType { get; set; }
	public string animationIdPrefab { get; set; }
}