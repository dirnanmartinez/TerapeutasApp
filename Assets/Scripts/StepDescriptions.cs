using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class StepDescriptions
{
	public int id { get; set; }
	public string Description { get; set; }
	public List<Entities> entities { get; set; }
}