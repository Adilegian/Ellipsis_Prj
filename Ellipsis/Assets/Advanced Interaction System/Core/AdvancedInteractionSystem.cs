using UnityEngine;

[RequireComponent(typeof(AIS_Lerper))]

public class AdvancedInteractionSystem : MonoBehaviour
{
    AIS_Lerper lerp;                                        //lerper script

    public bool StartPositionAndRotation = true;            //get the main position and rotation on start without manual input
    public Vector3 MainPosition;                            //the main position of the item
    public Vector3 MainRotation;                            //the main rotation of the item

    public Vector3 SecondaryPosition;                       //secondary position to lerp to
    public Vector3 SecondaryRotation;                       //secondary rotation to lerp to

    public AudioSource OpenAudio;                           //audio that plays when item is interacted with for the first time (like: open door)
    public AudioSource CloseAudio;                          //audio that plays when item is interacted with for the second time (like: close door)
    public AudioSource LockedAudio;                         //audio that plays when item is locked and interacted with

    public float OpenSpeed = 20f;                           //the speed of the lerp while opening
    public float CloseSpeed = 20f;                          //the speed of the lerp while closing

    public float OpenRotationSpeed = 20f;                   //the speed of the rotation while lerping on open
    public float CloseRotationSpeed = 20f;                  //the speed of the rotation while lerping on close

    public bool locked = false;                             //can the object be opened

    public AdvancedInteractionSystem[] ConnectedScripts;    //other connected scripts that should be fired. This should only be set on the main object

    [HideInInspector]
    public int state;                                       //which state is the object currently on: 1 or 2

    [HideInInspector]
    public GameObject go;                                   //dynamically created game object for lerping purposes

    [HideInInspector]
    public bool interactable = true;                        //flag whether the item can currently be interacted with


    // Start is called before the first frame update
    void Start()
    {
        lerp = GetComponent<AIS_Lerper>();
        lerp.LerpRotation = true;
        state = 1;

        //disable the lerper for performance
        lerp.enabled = false;

        if(StartPositionAndRotation){
            MainPosition = transform.localPosition;
            MainRotation = new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        }
    }

    //auto toggle open/close depending on state
    public void AutoToggle(){
        
        //if item is locked
        //play sound and retur
        if(locked){
            try{
                LockedAudio.Play();
            }catch{}
            return;
        }

        //if can't currently be interacted with then return
        if(!interactable){
            return;
        }

        //destroy the dynamically generated positional game object
        Destroy(go);

        //re-enable the lerper
        lerp.enabled = true;

        //trigger a state according to current state
        if(state == 1){
            SecondaryState();
        }else{
            MainState();
        }

        //loop all the connected scripts and trigger them
        foreach(var script in ConnectedScripts){
            script.AutoToggle();
        }
    }

    //the main state of the object before interaction, most probably closed
    void MainState(){
        
        //if object is locked, play locked audio and return
        if(locked){
            try{
                LockedAudio.Play();
            }catch{}
            return;
        }

        //flag as can't be interacted with until state is finished
        //destroy the old positional gameobject
        interactable = false;
        Destroy(go);

        //set the state
        state = 1;

        //create a new positional object
        //set it as child of the object
        //set the local position
        //set the local rotation
        go = new GameObject();
        go.name = "A.I.S. position lerp";

        if(transform.parent != null){
            go.transform.parent = transform.parent;
        }

        go.transform.localPosition = MainPosition;

        //set the correct transform, rotations and positions to the lerper script
        lerp.EndPos = go.transform;
        lerp.EndPos.position = go.transform.position;
        lerp.EndPos.localRotation = Quaternion.Euler(MainRotation.x, MainRotation.y, MainRotation.z);
        
        //play and stop the appropriate audios
        //inside try-catch to avoid errors if null
        try{
            CloseAudio.Play();
            OpenAudio.Stop();
        }catch{}
        
        //set the speed of the lerper
        lerp.lerpTime = CloseSpeed;
        lerp.QuatLerpTime = CloseRotationSpeed;

        //start lerping
        lerp.StartLerp = true;
    }

    //the secondary state of the object after interaction, most probably opened
    void SecondaryState(){

        if(locked){
            try{
                LockedAudio.Play();
            }catch{}
            return;
        }

        interactable = false;
        Destroy(go);

        state = 2;
        
        go = new GameObject();
        go.name = "A.I.S. position lerp";
        
        if(transform.parent != null){
            go.transform.parent = transform.parent;
        }
        
        go.transform.localPosition = SecondaryPosition;
        
        lerp.EndPos = go.transform;
        lerp.EndPos.position = go.transform.position;
        lerp.EndPos.localRotation = Quaternion.Euler(SecondaryRotation.x, SecondaryRotation.y, SecondaryRotation.z);
        
        try{
            OpenAudio.Play();
            CloseAudio.Stop();
        }catch{}
        
        lerp.lerpTime = OpenSpeed;
        lerp.QuatLerpTime = OpenRotationSpeed;

        lerp.StartLerp = true;
    }
}
