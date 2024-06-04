using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class ItemStatusPer_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/GameResources/ItemStatusPer.xlsx";
	private static readonly string exportPath = "Assets/GameResources/ItemStatusPer.asset";
	private static readonly string[] sheetNames = { "ItemStatusPer", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			ItemStatusPer data = (ItemStatusPer)AssetDatabase.LoadAssetAtPath (exportPath, typeof(ItemStatusPer));
			if (data == null) {
				data = ScriptableObject.CreateInstance<ItemStatusPer> ();
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

					ItemStatusPer.Sheet s = new ItemStatusPer.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						ItemStatusPer.Param p = new ItemStatusPer.Param ();
						
					cell = row.GetCell(0); p.ItemType = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.ItemRank1 = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.ItemRank2 = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.ItemRank3 = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.ItemRank4 = (float)(cell == null ? 0 : cell.NumericCellValue);
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
