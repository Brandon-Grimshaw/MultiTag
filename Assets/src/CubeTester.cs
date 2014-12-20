using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class CubeTester : MonoBehaviour {

	public KeyCode XToggleCode;
	public KeyCode YToggleCode;

	public List<GameObject> Cubes;

	public int x = 0, y = 0;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(XToggleCode)) {
			x = (x + 1) % 3;
		}

		if (Input.GetKeyDown(YToggleCode)) {
			y = (y + 1) % 3;
		}

		var cubes = Multitag.GetGameObjectsWithTags(new String[] { 'x' + x.ToString(), 'y' + y.ToString() });
		foreach (var cube in cubes) {
			cube.GetComponent<MeshRenderer>().enabled = true;
		}

		foreach (var cube in Cubes.Where(that => !cubes.Contains(that))) {
			cube.GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
