using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreCubes : MonoBehaviour
{
	public GameObject cubePrefab;

	public void Cube()
	{	
			Instantiate(cubePrefab);
	}
	
}
