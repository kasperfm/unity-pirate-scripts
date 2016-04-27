/*
    Dette script kan skifte mellem forskellige kameraer i scenen.
    Skrevet af: Kasper Færch Mikkelsen - Coding Pirates Viborg
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeCamera : MonoBehaviour {
	public List<Camera> cameras = new List<Camera>();
	private int currentCamera = 0;

	public KeyCode shiftButton = KeyCode.Tab;

	void Update () {
		if(Input.GetKeyUp(shiftButton)){
			if(cameras.Count > 1){
				for(int i = 0; i < cameras.Count; i++){
					if(i == currentCamera){
						cameras[i].enabled = true;
					}else{
						cameras[i].enabled = false;
					}
				}

				if(currentCamera >= cameras.Count -1){
					currentCamera = 0;
				}else{
					currentCamera++;
				}
			}
		}
	}
}
