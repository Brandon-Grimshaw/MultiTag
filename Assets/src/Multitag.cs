using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class Multitag : MonoBehaviour {
	
	private static List<Multitag> taggers = null;
	
	// Used only for inspector convenience, runtime interactions should be directed to the TagsSet HashSet
	[SerializeField]
	private List<string> Tags;

	// Actual tags storage, since it'll be searched a lot
	public HashSet<string> TagsSet;
	
	void Awake() {
		if (taggers == null) {
			taggers = new List<Multitag>();
		}
		
		taggers.Add(this);
		TagsSet = new HashSet<string>(Tags);
	}
	
	public static IEnumerable<GameObject> FindGameObjectsWithTag(string tag) {
		if (taggers == null) return null;
	
		return taggers.Where(x => x.TagsSet.Contains(tag)).Select(x => x.gameObject);
	}
	
	public static IEnumerable<GameObject> FindGameObjectsWithTags(IEnumerable<string> tags, bool loose = false) {
		if (taggers == null) return null;
		
		return taggers
			.Where(x => loose ? x.TagsSet.Intersect(tags).Any() : false)
			.Select(x => x.gameObject);
	}
}
