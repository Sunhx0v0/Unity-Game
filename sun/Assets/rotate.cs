using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start () {
    }
    // Update is called once per frame
    public int speed = 20;
	public Vector3 e = Vector3.forward;

	// Update is called once per frame
	void Update () {
		Quaternion q = Quaternion.AngleAxis (speed * Time.deltaTime, e);
		transform.localRotation *= q; 
	}
}
