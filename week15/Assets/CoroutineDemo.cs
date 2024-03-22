using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineDemo : MonoBehaviour {

	private IEnumerator<int> gen1,gen2;

	// Use this for initialization
	void Start () {
		gen1 = Generator (0);
		gen2 = Generator (100);
		print ("exec start work or not ?");
	}
	
	// Update is called once per frame
	void Update () {
		if (gen1.MoveNext ())
			print ("gen1: " + gen1.Current);
		if (gen2.MoveNext ())
			print ("gen2: " + gen2.Current);
	}

	IEnumerator<int> Generator(int start) {
		print ("start work! " + start);
		yield return start;
		for (int i = 1; i <= 10; i++) {
			print ("next work! " + start + i);
			yield return start + i;
		}
	}
}