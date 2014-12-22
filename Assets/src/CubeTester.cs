using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class CubeTester : MonoBehaviour {

	public KeyCode XToggleCode;
	public KeyCode YToggleCode;

	public List<GameObject> Cubes;
	
	public Text text;
	
	public bool testPerformance = false;

	private int x = 0, y = 0;
	
	private void interact() {
		if (Input.GetKeyDown(XToggleCode)) {
			x = (x + 1) % 3;
		}
		
		if (Input.GetKeyDown(YToggleCode)) {
			y = (y + 1) % 3;
		}
		
		// change cubes
	}
	
	private void test() {
		
		x = UnityEngine.Random.Range(0, 3); 
		y = UnityEngine.Random.Range(0, 3);
		var array = new string[] { 'x' + x.ToString(), 'y' + y.ToString() };
	
		Stopwatch stop = Stopwatch.StartNew();
		
		var result = Multitag.FindGameObjectsWithTags(array, true).ToList(); // ToList to force execution
		
		stop.Stop();
		print("x: " + array[0] + ", y: " + array[1] + ", search time: " + stop.Elapsed.ToString());
		
		foreach (var obj in Cubes) {
			obj.GetComponent<MeshRenderer>().enabled = false;
		}
		
		foreach (var obj in result) {
			obj.GetComponent<MeshRenderer>().enabled = true;
		}
	}
	
	void Start() {
		if (testPerformance) {
			foreach (var cube in Cubes) {
				for (int i = 0; i < 100000; i++)
					cube.GetComponent<Multitag>().TagsSet.Add(generateString());
			}
		}
	}
	
	string generateString() {
		var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		var random = new System.Random();
		return new string(
			Enumerable.Repeat(chars, 8)
			.Select(s => s[random.Next(s.Length)])
			.ToArray());
	}
	
	// Update is called once per frame
	void Update () {
		
		if (testPerformance) test();
		else interact();
		
//		var cubes = Multitag.GetGameObjectsWithTags(new String[] { 'x' + x.ToString(), 'y' + y.ToString() });
//		foreach (var cube in cubes) {
//			cube.GetComponent<MeshRenderer>().enabled = true;
//		}

//		foreach (var cube in Cubes.Where(that => !cubes.Contains(that))) {
//			cube.GetComponent<MeshRenderer>().enabled = false;
//		}
	}
}
