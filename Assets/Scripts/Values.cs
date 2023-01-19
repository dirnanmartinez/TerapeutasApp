using Newtonsoft.Json;
using System;

[Serializable]
public class Values
{
	[JsonProperty(PropertyName = "id")]
	public string Id { get; set; }

	[JsonProperty(PropertyName = "id")]
	public int id { get; set; }

	[JsonProperty(PropertyName = "name")]
	public string Name { get; set; }

	[JsonProperty(PropertyName = "description")]
	public string Description { get; set; }

	[JsonProperty(PropertyName = "creationDate")]
	public string CreationDate { get; set; }

	[JsonProperty(PropertyName = "finalMessageOK")]
	public string finalMessageOK { get; set; }

	[JsonProperty(PropertyName = "finalMessageError")]
	public string FinalMessageError { get; set; }

	[JsonProperty(PropertyName = "maxTime")]
	public string MaxTime { get; set; }

	[JsonProperty(PropertyName = "firstStep")]
	public string FirstStep { get; set; }

	[JsonProperty(PropertyName = "activityImage")]
	public string ActivityImage { get; set; }

	[JsonProperty(PropertyName = "initialHelp")]
	public string InitialHelp { get; set; }

	[JsonProperty(PropertyName = "finalMessage")]
	public string FinalMessage { get; set; }


}
