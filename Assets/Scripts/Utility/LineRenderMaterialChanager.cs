using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRenderMaterialChanager : MonoBehaviour
{
    public Material[] materials;
    [Range(0, 32)]
    public int materialIndex = 0;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ChanagedMaterial(int index)
    {
        if (index < 0 || index >= materials.Length)
        {
            Debug.LogError("Provided index is incorrect", this);
            return;
        }

        //Get Materials from renderer
        var rendererMaterials = lineRenderer.materials;

        if (materialIndex > rendererMaterials.Length)
        {
            Debug.LogError("Provided material index is incorrect", this);
            return;
        }

        //Chanage material
        rendererMaterials[materialIndex] = materials[index];
        //Assinged chanaged materials back to renderer
        lineRenderer.materials = rendererMaterials;
    }
}