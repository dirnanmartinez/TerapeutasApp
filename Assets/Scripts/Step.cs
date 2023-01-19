using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class Step
{

	public int id { get; set; }
	public string Groupal { get; set; }
	public string IsSupervised { get; set; }
	public string InteractiveSpaceName { get; set; }
	public List<StepDescriptions> stepDescriptions { get; set; }
	public string FeedbackPath { get; set; }
	public string PreviousStep { get; set; }
	public string Type { get; set; }

}