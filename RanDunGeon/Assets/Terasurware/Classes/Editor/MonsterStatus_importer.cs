using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class MonsterStatus_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/GameResources/MonsterStatus.xlsx";
	private static readonly string exportPath = "Assets/GameResources/MonsterStatus.asset";
	private static readonly string[] sheetNames = { "Chapter 0","Chapter 1","Chapter 2", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			MonsterStatus data = (MonsterStatus)AssetDatabase.LoadAssetAtPath (exportPath, typeof(MonsterStatus));
			if (data == null) {
				data = ScriptableObject.CreateInstance<MonsterStatus> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					MonsterStatus.Sheet s = new MonsterStatus.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						MonsterStatus.Param p = new MonsterStatus.Param ();
						
					cell = row.GetCell(0); p.Index = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.LV = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.HP = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.AD = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.Df = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.CriP = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.CriD = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.ED = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.DotsED = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.Gold = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.ItemRank = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(11); p.Image = (int)(cell == null ? 0 : cell.NumericCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
