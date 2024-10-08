using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class ItemTypePer_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/GameResources/ItemTypePer.xlsx";
	private static readonly string exportPath = "Assets/GameResources/ItemTypePer.asset";
	private static readonly string[] sheetNames = { "ItemTypePer", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			ItemTypePer data = (ItemTypePer)AssetDatabase.LoadAssetAtPath (exportPath, typeof(ItemTypePer));
			if (data == null) {
				data = ScriptableObject.CreateInstance<ItemTypePer> ();
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

					ItemTypePer.Sheet s = new ItemTypePer.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						ItemTypePer.Param p = new ItemTypePer.Param ();
						
					cell = row.GetCell(0); p.RewardRank = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.HPPer = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.ADPer = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.DFPer = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.HPAdd = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.ADAdd = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.DfAdd = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.CriPAdd = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.CriDAdd = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.EDAdd = (float)(cell == null ? 0 : cell.NumericCellValue);
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
