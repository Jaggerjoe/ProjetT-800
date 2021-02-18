using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

//public struct RegisteredArms
//{
//    public Arm s_Real;
//    public Arm s_Hidden;

//}

public class ThrowingTrajectory : MonoBehaviour
{

    //public bool isShowing = false;
  
    //private static bool m_Charging;

    //[SerializeField]
    //private GameObject m_Arm;
    //[SerializeField]
    //private Transform m_RefArm;

    //private Scene m_MainScene;
    //private Scene m_PhysicsScene;

    //[SerializeField]
    //private GameObject m_Marker;

    //private List<GameObject> m_MarkersList = new List<GameObject>();
    //private Dictionary<string, RegisteredArms> m_ArmDictionnary = new Dictionary<string, RegisteredArms>();

    //[SerializeField]
    //private GameObject m_ObjectsToSpawn;

    //private void Start()
    //{
    //    Physics.autoSimulation = false;

    //    m_MainScene = SceneManager.GetActiveScene();
    //    m_PhysicsScene = SceneManager.CreateScene("Physics_Scene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));

    //    PreparePhysicsScene();

    //}

    //private void FixedUpdate()
    //{
    //    if ( isShowing)
    //    {
    //        ShowTrajectory();
    //    }
    //    m_MainScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    //}

    //public void RegisteredArms (Arm p_Arm)
    //{
    //    if(m_ArmDictionnary.ContainsKey(p_Arm.gameObject.name))
    //    {
    //        m_ArmDictionnary[p_Arm.gameObject.name] = new RegisteredArms();
    //    }

    //    var l_Arms = m_ArmDictionnary[p_Arm.gameObject.name];
    //    if(string.Compare(p_Arm.gameObject.scene.name, m_PhysicsScene.name) == 0)
    //    {
    //        l_Arms.s_Hidden = p_Arm;
    //    }
    //    else
    //    {
    //        l_Arms.s_Real = p_Arm;
    //    }

    //    m_ArmDictionnary[p_Arm.gameObject.name] = l_Arms;
    //}

    //public void PreparePhysicsScene()
    //{
    //    SceneManager.SetActiveScene(m_PhysicsScene);

    //    GameObject l_Obj = GameObject.Instantiate(m_ObjectsToSpawn);
    //    l_Obj.transform.name = "ReferenceArm";
    //    l_Obj.GetComponent<Arm>().IsReference = true;
    //    Destroy(l_Obj.GetComponent<MeshRenderer>());

    //    SceneManager.SetActiveScene(m_MainScene);
    //}

    //public void CreateMarkers()
    //{
    //    foreach (var l_ArmType in m_ArmDictionnary)
    //    {
    //        var l_Arms = l_ArmType.Value;
    //        Arm l_Hidden = l_Arms.s_Hidden;

    //        GameObject l_Obj = GameObject.Instantiate(m_Marker, l_Hidden.transform.position, Quaternion.identity);
    //        l_Obj.transform.localScale = new Vector3(.3f, .3f, .3f);
    //        m_MarkersList.Add(l_Obj);

    //    }
    //}

    //public void ShowTrajectory()
    //{
    //    SynchArms();

    //    m_ArmDictionnary["ReferenceArm"].s_Hidden.transform.rotation = m_RefArm.rotation;
    //    m_ArmDictionnary["ReferenceArm"].s_Hidden.GetComponent<Rigidbody>().velocity = m_RefArm.TransformDirection(Vector3.up * 15f);
    //    m_ArmDictionnary["ReferenceArm"].s_Hidden.GetComponent<Rigidbody>().useGravity = true;

    //    int l_Steps = (int)(2f / Time.deltaTime);
    //    for (int i = 0; i < l_Steps; i++)
    //    {
    //        m_PhysicsScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    //        CreateMarkers();
    //    }
       

    //}

    //public void SynchArms()
    //{
    //    foreach (var l_ArmType in m_ArmDictionnary)
    //    {
    //        var l_Arms = l_ArmType.Value;
    //        Arm l_Visual = l_Arms.s_Real;
    //        Arm l_Hidden = l_Arms.s_Hidden;

    //        var rb = l_Hidden.GetComponent<Rigidbody>();

    //        rb.velocity = Vector3.zero;
    //        rb.angularVelocity = Vector3.zero;

    //        l_Hidden.transform.position = l_Visual.transform.position;
    //        l_Hidden.transform.rotation = l_Visual.transform.rotation;

    //    }
    //}
}
   
