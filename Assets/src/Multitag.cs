using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class Multitag : MonoBehaviour {

	public List<string> TagsList;
	
	public static IEnumerable<GameObject> GetGameObjectsWithTag(string tag) {
		return (
				from obj in FindObjectsOfType<Multitag>() 
				where obj.TagsList.Contains(tag) 
				select obj.gameObject
			).ToList(); 
	}

	public static IEnumerable<GameObject> GetGameObjectsWithTags(IEnumerable<string> tags) {
		return (
				from obj in FindObjectsOfType<Multitag>() 
				where tags.All(x => obj.TagsList.Contains(x)) 
				select obj.gameObject
			).ToList();
	}
}
