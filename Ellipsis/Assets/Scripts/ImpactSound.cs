using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    public AudioSource impactSound;
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude > 1)
		{
			impactSound.Play();
		}
	}
}
