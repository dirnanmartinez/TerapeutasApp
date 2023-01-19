using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class InteractiveSpaces
{
	[JsonProperty(PropertyName = "id")]
	public int id { get; set; }

	[JsonProperty(PropertyName = "name")]
	public string Name { get; set; }

	[JsonProperty(PropertyName = "description")]
	public string Description { get; set; }

	[JsonProperty(PropertyName = "visibility")]
	public string Visibility { get; set; }

	[JsonProperty(PropertyName = "anchorId")]
	public string AnchorId { get; set; }

	[JsonProperty(PropertyName = "owner")]
	public string Owner { get; set; }
}