using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class Entity3D
{
	
	[JsonProperty(PropertyName = "id")]
	public int id { get; set; }

	[JsonProperty(PropertyName = "name")]
	public string Name { get; set; }

	[JsonProperty(PropertyName = "description")]
	public string Description { get; set; }

	[JsonProperty(PropertyName = "path")]
	public string Path { get; set; }

	[JsonProperty(PropertyName = "Animations")]
	public List<Animations> animations { get; set; }
	


}