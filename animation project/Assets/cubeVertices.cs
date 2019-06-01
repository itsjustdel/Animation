using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeVertices : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < GetComponent<MeshFilter>().mesh.vertexCount; i++)
        {
            GameObject c = GameObject.CreatePrimitive(PrimitiveType.Cube);
            c.transform.position = GetComponent<MeshFilter>().mesh.vertices[i];
            c.name = i.ToString();
        }
	}
	
}
