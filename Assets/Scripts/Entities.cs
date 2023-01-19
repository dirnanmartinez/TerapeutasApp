using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class Entities
{
	
	public string entityPath { get; set; }
	public string entityName { get; set; }
	public int x { get; set; }
	public int y { get; set; }
	public int z { get; set; }
	public int rotX { get; set; }
	public int rotY { get; set; }
	public int rotZ { get; set; }
	public int scaleX { get; set; }
	public int scaleY { get; set; }
	public int scaleZ { get; set; }
	public List<Actions> actions { get; set; }


}