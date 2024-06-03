using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterStatus : ScriptableObject
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
		
		public int Index;
		public int LV;
		public float HP;
		public float AD;
		public float Df;
		public float CriP;
		public float CriD;
		public float ED;
		public float DotsED;
		public int Gold;
		public int ItemRank;
		public int Image;
	}
}

