using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    public Vector3 Velocity {  get { return Rigidbody.velocity; } set { Rigidbody.velocity = value; } }
    public bool Armed { get; private set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        //Add material dynamicly, because setting material from editor doesn't work, unless reentering value of bouncinnes in play mode
        GetComponent<Collider>().material = new PhysicMaterial()
        {
            bounciness = 1f,
            dynamicFriction = 0f,
            bounceCombine = PhysicMaterialCombine.Maximum,
            staticFriction = 0f,
            frictionCombine = PhysicMaterialCombine.Maximum
        };
    }


    public void SetArmed(bool armed)
    {
        Armed = armed;
    }
}