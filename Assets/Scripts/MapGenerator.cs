using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
	{
		public GridVisualizer gridVisualizer;

		[Range(3,20)]
		public int width, length = 11;
		private MapGrid grid;
		private void Start()
		{
			grid = new MapGrid(width, length);
			gridVisualizer.VisualizeGrid(width, length);
		}
	}
