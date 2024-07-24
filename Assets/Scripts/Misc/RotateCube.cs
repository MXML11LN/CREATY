using UnityEngine;

namespace ColorDrawer
{
    public class RotateCube : MonoBehaviour
    {
        public Vector3 rotationAxis = Vector3.up; 
        public float rotationSpeed = 10f;        

        void Update() => transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}