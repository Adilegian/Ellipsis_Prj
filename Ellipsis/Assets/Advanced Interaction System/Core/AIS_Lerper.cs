using UnityEngine;

public class AIS_Lerper : MonoBehaviour
{
    [Tooltip("The Transform of the object to lerp to")]
	[HideInInspector]
	public Transform EndPos;
	
	[Header("LERPING TIMES")]
	[Space(20)]

	[Tooltip("The amount of time it takes for a lerp to finish. The greater the number the slower it is. 300 is 3 seconds and so on")]
	[HideInInspector]
	public float lerpTime = 5f;
	
	[Tooltip("The amount of time it takes for a rotation lerp to finish")]
	[HideInInspector]
	public float QuatLerpTime = 5f;
	
	[Tooltip("Enable/disable rotation lerping")]
	[HideInInspector]
	public bool LerpRotation = true;

	[Header("POSITIONAL OFFSETS")]
	[Space(20)]
	[HideInInspector]
	public float XOffset = 0f,
    YOffset = 0f,
    ZOffset = 0f;

	[Header("ROTATIONAL OFFSETS")]
	[Space(20)]
	[HideInInspector]
	public float XOffsetQuart = 0f,
	YOffsetQuart = 0f,
	ZOffsetQuart = 0f;
	
	private float currentLerpTime = 0f;
	private float perc;
	private float Quatperc;
	private bool _lerp;
	
	[HideInInspector]
	public bool IsActive = false;
    
	[HideInInspector]
    public bool ShouldStopLerp = false;
	
	private bool ShouldStopQuatLerp = false;

	AdvancedInteractionSystem ais;

	//property to trigger the lerping function
	public bool StartLerp {
		get{ return _lerp; }

		set{
			_lerp = value;
			if (_lerp){
				currentLerpTime = 0f;
				IsActive = true;
				ShouldStopLerp = false;
				ShouldStopQuatLerp = false;
				Lerping();
			}
		}
	}

	void Start()
	{
		ais = GetComponent<AdvancedInteractionSystem>();
	}
	
	//Update is called once per frame
	void Update () {
		if(IsActive){
			Lerping ();
		}
	}

	//the main lerping method
	void Lerping(){
		
		//increment timer once per frame
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}

		//lerp!
		perc = currentLerpTime / lerpTime;
		Quatperc = currentLerpTime / QuatLerpTime;

		if (!ShouldStopLerp)
			transform.position = Vector3.Lerp (transform.position, EndPos.position + new Vector3(XOffset, YOffset, ZOffset), perc);

		//only if checked, lerp rotation as well
		if (LerpRotation && !ShouldStopQuatLerp)
			transform.rotation = Quaternion.Lerp (transform.rotation, EndPos.rotation * Quaternion.Euler(XOffsetQuart, YOffsetQuart, ZOffsetQuart), Quatperc);

		//if distance between two goals of lerp is smaller than threshold then stop lerp
		if (Vector3.Distance (transform.position, EndPos.position) <= 0.01f){
			ShouldStopLerp = true;
			//if rotation lerping is enabled
			if (LerpRotation) {
				//if angle between two quaternions is smaller than threshold then stop
				if (Quaternion.Angle(transform.rotation, EndPos.rotation) <= 0.01f){
					ShouldStopQuatLerp = true;
					IsActive = false;
					Destroy(ais.go);
					ais.interactable = true;
					this.enabled = false;
				}
			//if lerp rotation disabled then stop the method
			}else {
				IsActive = false;
				Destroy(ais.go);
				ais.interactable = true;
				this.enabled = false;
			}
		}
	}
}
