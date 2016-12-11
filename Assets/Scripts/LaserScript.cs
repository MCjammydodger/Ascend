using UnityEngine;

public class LaserScript : MonoBehaviour {
    [SerializeField]
    private float maxAlpha;
    [SerializeField]
    private float minAlpha;

    private MeshRenderer meshRenderer;
    [SerializeField]
    private Material material;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
	// Update is called once per frame
	void Update () {
        material = meshRenderer.material;
        material.color = new Color(material.color.r, material.color.g, material.color.b, Random.Range(minAlpha, maxAlpha));
        meshRenderer.material = material;
	}
}
