using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ChanageMaterial : MonoBehaviour
{
    public Material[] materials;
    [Range(0,32)]
    public int materialIndex = 0;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChanagedMaterial(int index)
    {
        if(index < 0 || index >= materials.Length)
        {
            Debug.LogError("Provided index is incorrect", this);
            return;
        }

        //Get Materials from renderer
        var rendererMaterials = meshRenderer.materials;

        if(materialIndex > rendererMaterials.Length)
        {
            Debug.LogError("Provided material index is incorrect", this);
            return;
        }

        //Chanage material
        rendererMaterials[materialIndex] = materials[index];
        //Assinged chanaged materials back to renderer
        meshRenderer.materials = rendererMaterials;
    }
}
