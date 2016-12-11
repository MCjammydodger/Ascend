using UnityEngine;

public class Interactable : MonoBehaviour {
    private bool isOn = false;
    private bool playerIsInRange = false;
    private float lerpTime = 0;
    private float lerpInterval = -1;

    [SerializeField]
    private EffectsOfInteraction[] effects;
    [SerializeField]
    private float moveSpeed = 1;

    private void Start()
    {
        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i].behaviour == EffectsOfInteraction.Behaviour.Move || effects[i].behaviour == EffectsOfInteraction.Behaviour.AutoMove)
            {
                effects[i].initialPosition = effects[i].obj.transform.position;
                effects[i].initialRotation = effects[i].obj.transform.rotation;
            }
        }
    }
    private void Update()
    {

        if (Input.GetButtonUp("Interact") && playerIsInRange)
        {
            isOn = !isOn;
            UpdateVisuals(isOn);
            if (isOn)
            {
                if (lerpTime < 0)
                {
                    lerpTime = 0;
                }
                lerpInterval = 1;
            }
            else
            {
                if (lerpTime > 1)
                {
                    lerpTime = 1;
                }
                lerpInterval = -1;
            }
        }
        if (effects[0].behaviour == EffectsOfInteraction.Behaviour.AutoMove)
        {
            if (lerpTime >= 1)
            {
                lerpTime = 1;
                lerpInterval = -1;
            }
            else if (lerpTime <= 0)
            {
                lerpTime = 0;
                lerpInterval = 1;
            }
        }
        UpdateLerps();
    }
    private void UpdateLerps()
    {
        lerpTime += lerpInterval * Time.deltaTime * moveSpeed;
        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i].behaviour == EffectsOfInteraction.Behaviour.Move || effects[i].behaviour == EffectsOfInteraction.Behaviour.AutoMove)
            {
                effects[i].obj.transform.position = Vector3.Lerp(effects[i].initialPosition, effects[i].transformToMoveTo.position, lerpTime);
            }
        }
    }
    protected virtual void UpdateVisuals(bool isOn)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsInRange = false;
        }
    }
}

[System.Serializable]
public class EffectsOfInteraction
{
    public enum Behaviour { Move, AutoMove};
    public Behaviour behaviour;
    public GameObject obj;
    public Transform transformToMoveTo;
    
    [HideInInspector]
    public Vector3 initialPosition;
    [HideInInspector]
    public Quaternion initialRotation;
}