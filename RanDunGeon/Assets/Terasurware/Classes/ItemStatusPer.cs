using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemStatusPer : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public string ItemType;
		public float ItemRank1;
		public float ItemRank2;
		public float ItemRank3;
		public float ItemRank4;
	}
}

