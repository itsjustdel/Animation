using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmySpawn : MonoBehaviour {
    public int amount = 1;
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < amount; i++)
        {
            for (int j = 0; j < amount; j++)
            {


                GameObject g = new GameObject();
                BindPoseExample bpe = g.AddComponent<BindPoseExample>();
                ProceduralAnimator pA = g.AddComponent<ProceduralAnimator>();
                CharacterControllerProc ccp = g.AddComponent<CharacterControllerProc>();
                ccp.pA = pA;
                ccp.bPE = bpe;
                bpe.spawnPoint = Vector3.right * i * 2+ Vector3.forward* j * 2;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
