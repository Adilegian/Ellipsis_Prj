using UnityEngine;

public class lbeam : MonoBehaviour 
{

	public GameObject beam;
	public GameObject lights;

	void Start()
	{
		beam.SetActive(false);
		lights.SetActive(false);
	}

	void Update()
	{
		beamOn();
		beamOff();
		LightsOn();
		LightsOff();
	}

	void beamOn()
	{
		if (Input.GetMouseButtonDown(0))
        {
        	beam.SetActive(true);
        }
	}

	void beamOff()
	{
		if(Input.GetMouseButtonUp(0))
		{
			beam.SetActive(false);
		}
	}

	void LightsOn()
	{
		if (Input.GetMouseButtonDown(1))
        {
        	lights.SetActive(true);
        }
	}

	void LightsOff()
	{
		if (Input.GetMouseButtonUp(1))
        {
        	lights.SetActive(false);
        }
	}
}