using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RockPlacement : MonoBehaviour {

	public float minDistance, maxDistance;
	public int Sections;

	private List<Placement> placements;
	private List<Placement> path;

	internal class Placement
	{
		internal int Id;
		internal List<int> GoToList;
		internal bool[] Orientation;

		internal Placement(int id, bool left, bool middle, bool right, List<int> goToList)
		{
			Id = id;
			Orientation = new bool[]{left, middle, right};
			GoToList = goToList;
		}
		internal int Next()
		{
			int index = Random.Range (0, GoToList.Count);
			return GoToList [index] - 1;
		}
	}

	void Start () {
		placements = new List<Placement> (){ 
			new Placement (1, false, true, true, new List<int> (){2, 4}),
			new Placement (2, true, false, true, new List<int> (){3, 1, 4}),
			new Placement (3, false, true, false, new List<int> (){2}),
			new Placement (4, true, true, false, new List<int> () {1, 2}) };
		OrganizeSections ();
		CreateSections ();


	}

	private void OrganizeSections()
	{
		path = new List<Placement> ();
		path.Add (placements [1]);
		for (int i = 1; i < Sections; i++) {
			path.Add(placements[path[path.Count - 1].Next()]);				
		}
		if (path[path.Count - 1] != placements[1])
			path.Add (placements [1]);
	}

	private void CreateSections()
	{
		float distance = -8;
		GameObject go0, go1, go2;
		foreach (Placement tile in path) {

			if (tile.Orientation[0]){
				go0 = Instantiate (Resources.Load<GameObject> ("Rock"));
				go0.transform.position = new Vector3(distance, -2, 0);
				Debug.Log(tile.Id);
			}
			if (tile.Orientation[1]){
				go1 = Instantiate (Resources.Load<GameObject> ("Rock"));
				go1.transform.position = new Vector3(distance, 0, 0);
				Debug.Log(tile.Id);
			}
			if (tile.Orientation[2]){
				go2 = Instantiate (Resources.Load<GameObject> ("Rock"));
				go2.transform.position = new Vector3(distance, 2, 0);
				Debug.Log(tile.Id);
			}

			distance += Random.Range(minDistance, maxDistance);
		}
	}
}
