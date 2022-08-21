using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipRaytraceBary : MonoBehaviour
{

    public Transform rayOmitter;
    public Vector3 cameraRayOffset;
    public Vector3 mainRayOffset;
    public Vector3 trackNormal;
    public Vector3 cameraNormal;
   
    public float trackDistance;

    public float currentTrackDistance;
    public float nextTrackDistance;

    public bool trackDetected = false;

    void FixedUpdate()
    {
        var mainDetectionRay = new Ray(rayOmitter.position + mainRayOffset, -transform.up);
        var frontDetectionRay = new Ray(rayOmitter.position + cameraRayOffset, -transform.up);

        trackDistance = currentTrackDistance;


        RaycastHit hit;



        if (Physics.Raycast(mainDetectionRay, out hit, 50) && hit.transform.tag == "Track")
        {
            trackDetected = true;
            currentTrackDistance = Mathf.Clamp(hit.distance, 0, 50);

            MeshCollider meshCollider = hit.collider as MeshCollider;
            Mesh mesh = meshCollider.sharedMesh;
            Vector3[] normals = mesh.normals;
            int[] triangles = mesh.triangles;

            // Extract local space normals of the triangle we hit
            Vector3 n0 = normals[triangles[hit.triangleIndex * 3 + 0]];
            Vector3 n1 = normals[triangles[hit.triangleIndex * 3 + 1]];
            Vector3 n2 = normals[triangles[hit.triangleIndex * 3 + 2]];

            // interpolate using the barycentric coordinate of the hitpoint
            Vector3 baryCenter = hit.barycentricCoordinate;

            // Use barycentric coordinate to interpolate normal
            Vector3 interpolatedNormal = n0 * baryCenter.x + n1 * baryCenter.y + n2 * baryCenter.z;
            // normalize the interpolated normal
            interpolatedNormal = interpolatedNormal.normalized;

            // Transform local space normals to world space
            Transform hitTransform = hit.collider.transform;
            interpolatedNormal = hitTransform.TransformDirection(interpolatedNormal);

            // Display with Debug.DrawLine
            Debug.DrawRay(rayOmitter.position + mainRayOffset, -transform.up, Color.green, 3);

            trackNormal = interpolatedNormal;
        }


        else
        {
            trackDetected = false;
        }

        if (Physics.Raycast(frontDetectionRay, out hit, 50) && hit.transform.tag == "Track")
        {
            cameraNormal = hit.normal;

            // Display with Debug.DrawLine
            Debug.DrawRay(rayOmitter.position + cameraRayOffset, -transform.up, Color.green, 3);
        }
    }
}



