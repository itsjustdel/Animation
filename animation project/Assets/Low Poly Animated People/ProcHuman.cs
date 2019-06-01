using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcHuman : MonoBehaviour {

    List<Vector3> vertices = new List<Vector3>();
    List<int> trianglesShoes = new List<int>();
    List<Transform> bones = new List<Transform>();
    List<BoneWeight> boneWeights = new List<BoneWeight>();
    Mesh mesh;
   // public float footWidth = 0.1f;
    public  float footHeight = 0.1f;
    public  float footLength = 0.2f;

    //public float ankleLength = 0.1f;
    public float ankleWidth = 0.1f;

    public float shinLength = 0.4f;

    public float thighLength = 0.8f;
    public float thighWidth = 0.2f;

    public float waistWidth = 0.25f;

    // Use this for initialization
    void Start()
    {
        mesh = new Mesh();

        GameObject meshChild = new GameObject();
        meshChild.transform.parent = transform;
        SkinnedMeshRenderer skinnedRenderer = meshChild.AddComponent<SkinnedMeshRenderer>();

        MakeBones();
        MakeMesh();
        

        for (int i = 0; i < vertices.Count;i++)
        {
        //    GameObject c = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    c.transform.position = vertices[i];
        //    c.transform.localScale *= 0.01f;
        //    c.name = i.ToString();
        }

        //MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
       // MeshFilter meshFilter = meshChild.AddComponent<MeshFilter>();
      //  meshFilter.mesh = mesh;


        Material[] materials = new Material[2];
        materials[0] = Resources.Load("Brown") as Material;
        materials[1] = Resources.Load("Blue") as Material;

        skinnedRenderer.sharedMaterials = materials;
        skinnedRenderer.bones = bones.ToArray();
        
        
       // skinnedRenderer.sharedMesh = mesh;
    }

    void MakeBones()
    {
        //right ankle
        Vector3 rightAnklePos = Vector3.right * (waistWidth - ankleWidth*.5f) + Vector3.up*footHeight + Vector3.forward*ankleWidth*.5f;
        GameObject rightAnkle = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightAnkle.transform.position = rightAnklePos;
        rightAnkle.name = "Ankle bone";
        rightAnkle.transform.localScale *= 0.1f;
        

        //right ankle
        Vector3 rightKneePos = Vector3.right * (waistWidth - ankleWidth * .5f) + Vector3.up * (shinLength+ footHeight) + Vector3.forward * ankleWidth * .5f;
        GameObject rightKnee = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightKnee.transform.position = rightKneePos;
        rightKnee.name = "knee";
        rightKnee.transform.localScale *= 0.1f;

        //right ankle
        Vector3 hipsPos = Vector3.up * (shinLength + thighLength + footHeight);
        GameObject hips = GameObject.CreatePrimitive(PrimitiveType.Cube);
        hips.transform.position = hipsPos;
        hips.name = "hips";
        hips.transform.localScale *= 0.1f;

        GameObject genericRigParent = new GameObject();
        genericRigParent.transform.parent = transform;
        hips.transform.parent = genericRigParent.transform;
        rightKnee.transform.parent = hips.transform;
        rightAnkle.transform.parent = rightKnee.transform;

        //bones.Add(genericRigParent.transform);
        bones.Add(hips.transform);
        bones.Add(rightKnee.transform);
       // bones.Add(rightAnkle.transform);

        Matrix4x4[] bindPoses = new Matrix4x4[bones.Count];
        for (int i = 0; i < bindPoses.Length; i++)
        {
            bindPoses[i] = bones[i].worldToLocalMatrix * transform.localToWorldMatrix;
        }

        mesh.bindposes = bindPoses;
    }
	
    void MakeMesh()
    {

        #region foot
        //start in one corner of the foot and extrude round in a square
        Vector3 pinkyToe = Vector3.right * waistWidth +Vector3.forward*footLength ;
        Vector3 bigToe = pinkyToe - Vector3.right * ankleWidth;
        Vector3 heelInside = Vector3.right * (waistWidth - ankleWidth);
        Vector3 heelOutside = Vector3.right * waistWidth;

        //upper layer of foot
        Vector3 insideHeelUpper = heelInside + Vector3.up * footHeight;
        Vector3 outsideHeelUpper = heelOutside + Vector3.up * footHeight;

        Vector3 outsideBridge = outsideHeelUpper + Vector3.forward * ankleWidth;
        Vector3 insideBridge = insideHeelUpper + Vector3.forward * ankleWidth;

        //foot
        vertices.Add(pinkyToe);//0
        vertices.Add(bigToe);//1
        vertices.Add(heelInside);//2
        vertices.Add(heelOutside);//3
        vertices.Add(outsideBridge);//4
        vertices.Add(insideBridge);//5
        vertices.Add(insideHeelUpper);//6
        vertices.Add(outsideHeelUpper);//7

        //triangles
        //front
        trianglesShoes.Add(0);
        trianglesShoes.Add(4);
        trianglesShoes.Add(1);

        trianglesShoes.Add(4);
        trianglesShoes.Add(5);
        trianglesShoes.Add(1);
        //outside
        trianglesShoes.Add(0);
        trianglesShoes.Add(3);
        trianglesShoes.Add(4);

        trianglesShoes.Add(7);
        trianglesShoes.Add(4);
        trianglesShoes.Add(3);

        //back
        trianglesShoes.Add(7);
        trianglesShoes.Add(3);
        trianglesShoes.Add(6);        

        trianglesShoes.Add(6);
        trianglesShoes.Add(3);
        trianglesShoes.Add(2);

        //inside
        trianglesShoes.Add(1);
        trianglesShoes.Add(6);
        trianglesShoes.Add(2);

        trianglesShoes.Add(1);
        trianglesShoes.Add(5);
        trianglesShoes.Add(6);

        //bottom
        trianglesShoes.Add(0);
        trianglesShoes.Add(1);
        trianglesShoes.Add(2);

        trianglesShoes.Add(0);
        trianglesShoes.Add(2);
        trianglesShoes.Add(3);

        //weights
        
        for (int i = 0; i < vertices.Count; i++)
        {
            //link al foot vertices to bone [2] (ankle bone)
            boneWeights.Add(new BoneWeight
            {
                boneIndex0 = 0,                
                weight0 = 1
            });
        }

        #endregion
        /*
        #region legs
        //right knee

        Vector3 outsideKneeFront = outsideBridge + Vector3.up * shinLength;
        Vector3 insideKneeFront = insideBridge + Vector3.up * shinLength;
        Vector3 insideKneeBack = insideHeelUpper + Vector3.up * shinLength;
        Vector3 outsideKneeBack = outsideHeelUpper + Vector3.up * shinLength;

        //knee
        vertices.Add(outsideKneeFront);//8
        vertices.Add(insideKneeFront);//9
        vertices.Add(insideKneeBack);//10
        vertices.Add(outsideKneeBack);//11

        List<int> trianglesLegs = new List<int>();
        //shin
        //front
        trianglesLegs.Add(5);
        trianglesLegs.Add(4);
        trianglesLegs.Add(8);

        trianglesLegs.Add(5);
        trianglesLegs.Add(8);
        trianglesLegs.Add(9);
        //inside
        trianglesLegs.Add(9);        
        trianglesLegs.Add(10);
        trianglesLegs.Add(5);

        trianglesLegs.Add(10);
        trianglesLegs.Add(6);
        trianglesLegs.Add(5);
        //rear
        trianglesLegs.Add(6);
        trianglesLegs.Add(11);
        trianglesLegs.Add(7);

        trianglesLegs.Add(10);        
        trianglesLegs.Add(11);
        trianglesLegs.Add(6);
        //outside

        trianglesLegs.Add(4);
        trianglesLegs.Add(7);
        trianglesLegs.Add(8);

        trianglesLegs.Add(8);
        trianglesLegs.Add(7);
        trianglesLegs.Add(11);

        //thigh
        //right hip
        Vector3 outsideHipFront = outsideKneeFront + Vector3.up * thighLength;
        Vector3 insideGroinFront = Vector3.up * (footHeight + shinLength + thighLength)  + Vector3.forward*ankleWidth;
        Vector3 insideGroinBack = insideGroinFront - Vector3.forward * thighWidth;
        Vector3 outsideHipBack = outsideHipFront - Vector3.forward * thighWidth;
        
        //hip/groin
        vertices.Add(outsideHipFront);
        vertices.Add(insideGroinFront);
        vertices.Add(insideGroinBack);
        vertices.Add(outsideHipBack);

        //front thigh
        trianglesLegs.Add(9);
        trianglesLegs.Add(8);        
        trianglesLegs.Add(13);

        trianglesLegs.Add(12);
        trianglesLegs.Add(13);
        trianglesLegs.Add(8);

        //inside thigh
        
        trianglesLegs.Add(10);
        trianglesLegs.Add(9);
        trianglesLegs.Add(13);

        trianglesLegs.Add(13);
        trianglesLegs.Add(14);        
        trianglesLegs.Add(10);

        //rear
        trianglesLegs.Add(11);
        trianglesLegs.Add(10);        
        trianglesLegs.Add(14);

        trianglesLegs.Add(11);        
        trianglesLegs.Add(14);
        trianglesLegs.Add(15);
        //outside
        trianglesLegs.Add(15);
        trianglesLegs.Add(12);        
        trianglesLegs.Add(8);

        trianglesLegs.Add(11);
        trianglesLegs.Add(15);        
        trianglesLegs.Add(8);
        #endregion
    */
        mesh = new Mesh();
        mesh.vertices = vertices.ToArray();        
        mesh.subMeshCount = 2;
        mesh.SetTriangles(trianglesShoes.ToArray(), 0);
       // mesh.SetTriangles(trianglesLegs.ToArray(), 1);
        mesh.boneWeights = boneWeights.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();




    }

    
}
