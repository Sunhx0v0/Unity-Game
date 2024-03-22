using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetHit();
    }

    public void GetHit() {
        if (Input.GetButtonDown("Fire1")) {
			//create ray, origin is camera, and direction to mousepoint
			Camera ca = Camera.main;
			Ray ray = ca.ScreenPointToRay(Input.mousePosition);

			//Return the ray's hit
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
                
                hit.transform.gameObject.SetActive(false);
			}
            else{
                
            }
		}
    }
}
