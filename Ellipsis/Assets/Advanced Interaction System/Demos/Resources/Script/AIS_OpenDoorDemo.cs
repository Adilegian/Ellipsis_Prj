using UnityEngine;

public class AIS_OpenDoorDemo : MonoBehaviour
{
    AdvancedInteractionSystem ais;

    // Start is called before the first frame update
    void Start()
    {
        ais = GetComponent<AdvancedInteractionSystem>();    
    }

    // Update is called once per frame
    void Update()
    {
        //on space button press
        //trigger toggle door open/close
        if(Input.GetKeyDown(KeyCode.Space)){
            ais.AutoToggle();
        }
    }
}
