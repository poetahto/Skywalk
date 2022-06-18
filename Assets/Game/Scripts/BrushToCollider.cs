#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sabresaurus.SabreCSG;
using UnityEditor;

public class BrushToCollider : MonoBehaviour, IPostBuildListener 
{
	[SerializeField]
	bool generateCollider = true;
	[SerializeField]
	bool isTrigger;
	[SerializeField]
	bool preferBoxCollider = true;

	[SerializeField]
	List<Component> componentsToCopy = new List<Component>();

	public void OnBuildFinished(Transform meshGroupTransform)
	{
		// Create a new object to hold the collider/components
		GameObject newObject = new GameObject(this.name);
		newObject.transform.SetParent(meshGroupTransform);
		newObject.transform.position = this.transform.position;
		newObject.transform.rotation = this.transform.rotation;
		newObject.transform.localScale = this.transform.localScale;

		// If the user wants a collider generate one with the bounds of the brush
		if(generateCollider)
		{
			Brush brush = GetComponent<Brush>();
			if(preferBoxCollider)
			{
				Bounds localBounds = brush.GetBounds();
				BoxCollider boxCollider = newObject.AddComponent<BoxCollider>();
				boxCollider.size = localBounds.extents;
				boxCollider.isTrigger = isTrigger;
			}
			else
			{
				Polygon[] polygons = brush.GetPolygons();
				Mesh mesh = new Mesh();
				List<int> polygonIndices = new List<int>();
				// Convert the source polygons into a Unity mesh
				BrushFactory.GenerateMeshFromPolygons(polygons, ref mesh, out polygonIndices);
				// Create a mesh collider and assign the mesh to it
				MeshCollider meshCollider = newObject.AddComponent<MeshCollider>();
				meshCollider.sharedMesh = mesh;
				meshCollider.convex = true; // Brushes are convex so mesh collider might as well be too
				meshCollider.isTrigger = isTrigger;
			}
		}

		// Copy all referenced components to the new object
		for (int i = 0; i < componentsToCopy.Count; i++) 
		{
			if(componentsToCopy[i] != null)
			{
				Component newComponent = newObject.AddComponent(componentsToCopy[i].GetType());
				EditorUtility.CopySerialized(componentsToCopy[i], newComponent);
			}
		}
	}
}
#endif