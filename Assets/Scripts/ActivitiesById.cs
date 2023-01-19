using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class ActivitiesById
{
	[JsonProperty(PropertyName = "id")]
	public int Id { get; set; }

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

	[JsonProperty(PropertyName = "firstStepId")]
	public string FirstStepId { get; set; }

	[JsonProperty(PropertyName = "lastStep")]
	public string LastStep { get; set; }

	[JsonProperty(PropertyName = "lastStepId")]
	public string LastStepId { get; set; }

	[JsonProperty(PropertyName = "activityImage")]
	public string ActivityImage { get; set; }

	[JsonProperty(PropertyName = "initialHelp")]
	public string InitialHelp { get; set; }

	[JsonProperty(PropertyName = "finalMessage")]
	public string FinalMessage { get; set; }

	[JsonProperty(PropertyName = "owner")]
	public string Owner { get; set; }

	[JsonProperty(PropertyName = "noStepsDefined")]
	public string NoStepsDefined { get; set; }

	

}