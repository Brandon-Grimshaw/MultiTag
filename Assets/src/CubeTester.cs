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
	public bool disjunctive = false;
	public int randomTagCount = 10000;
	public float delay = 0.5f;

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
	
	private IEnumerator test() {
		while (this.enabled) {
			x = UnityEngine.Random.Range(0, 3); 
			y = UnityEngine.Random.Range(0, 3);
			var array = new string[] { 'x' + x.ToString(), 'y' + y.ToString() };
		
			Stopwatch stop = Stopwatch.StartNew();
			
			var result = Multitag.FindGameObjectsWithTags(array, disjunctive).ToList(); // ToList to force execution
			
			stop.Stop();
			
			var perf = "x: " + array[0] + ", y: " + array[1] + ", search time: " + stop.Elapsed.ToString();
			print(perf);
			text.text = perf;
			
			foreach (var obj in Cubes) {
				obj.GetComponent<MeshRenderer>().enabled = false;
			}
			
			foreach (var obj in result) {
				obj.GetComponent<MeshRenderer>().enabled = true;
			}
			
			yield return new WaitForSeconds(delay);
		}
	}
	
	void Start() {
		if (testPerformance) {
			foreach (var cube in Cubes) {
				for (int i = 0; i < randomTagCount; i++)
					cube.GetComponent<Multitag>().TagsSet.Add(generateString());
			}
			
			StartCoroutine(test());
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
		if (!testPerformance) interact();
		
//		var cubes = Multitag.GetGameObjectsWithTags(new String[] { 'x' + x.ToString(), 'y' + y.ToString() });
//		foreach (var cube in cubes) {
//			cube.GetComponent<MeshRenderer>().enabled = true;
//		}

//		foreach (var cube in Cubes.Where(that => !cubes.Contains(that))) {
//			cube.GetComponent<MeshRenderer>().enabled = false;
//		}
	}
}
