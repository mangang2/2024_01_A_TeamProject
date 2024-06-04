using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemTypePer : ScriptableObject
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
		
		public int RewardRank;
		public float HPPer;
		public float ADPer;
		public float DFPer;
		public float HPAdd;
		public float ADAdd;
		public float DfAdd;
		public float CriPAdd;
		public float CriDAdd;
		public float EDAdd;
	}
}

