using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class Activity2User
{
	[JsonProperty(PropertyName = "activityId")]
	public int Activityid { get; set; }

	[JsonProperty(PropertyName = "user")]
	public string User { get; set; }

	[JsonProperty(PropertyName = "name")]
	public string Name { get; set; }

	[JsonProperty(PropertyName = "description")]
	public string Description { get; set; }

	[JsonProperty(PropertyName = "creationDate")]
	public string CreationDate { get; set; }

	[JsonProperty(PropertyName = "finalMessageOK")]
	public string FinalMessageOK { get; set; }

	[JsonProperty(PropertyName = "finalMessageError")]
	public string FinalMessageError { get; set; }

	[JsonProperty(PropertyName = "maxTime")]
	public string MaxTime { get; set; }

	[JsonProperty(PropertyName = "owner")]
	public string Owner { get; set; }

}