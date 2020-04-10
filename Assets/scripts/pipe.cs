using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]

public class pipe: MonoBehaviour
{
    public int definition = 16;
    public float length = 2.0f;
    public float edgeLength = 0.1f;
    public float radius = 0.5f;
    public float thickness = 0.03f;

    [ContextMenu("Build")]
    public void Build()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        filter.sharedMesh = CreateStraightPipe2(radius, length, edgeLength, thickness, definition);

    }

    // Start is called before the first frame update
    void Start()
    {
        Build();
    }

    // Update is called once per frame
    void Update()
    {

    }
    static public Mesh CreateStraightPipe2(float radius, float length, float edgeLength, float thickness, int definition)
    {
        Mesh mesh = new Mesh();

        int vertCount = (definition + 1) * 16;

        Vector3[] positions = new Vector3[vertCount];
        Vector3[] normals = new Vector3[vertCount];
        Vector2[] uvs = new Vector2[vertCount];
        Color[] colors = new Color[vertCount];

        float halfThickness = thickness * 0.5f;
        float halfLength = length * 0.5f;
        // greater edge. (radius, length)
        Vector2[] sectionPos = new Vector2[8];
        sectionPos[0] = new Vector2(radius + halfThickness * 3.0f, -halfLength);
        sectionPos[1] = new Vector2(radius + halfThickness * 3.0f, -halfLength + edgeLength + thickness);
        sectionPos[2] = new Vector2(radius + halfThickness, -halfLength + edgeLength + thickness);
        sectionPos[3] = new Vector2(radius + halfThickness, halfLength);
        sectionPos[4] = new Vector2(radius - halfThickness, halfLength);
        sectionPos[5] = new Vector2(radius - halfThickness, -halfLength + edgeLength);
        sectionPos[6] = new Vector2(radius + halfThickness, -halfLength + edgeLength);
        sectionPos[7] = new Vector2(radius + halfThickness, -halfLength);

        Vector2[] sectionNormal = new Vector2[8];
        sectionNormal[0] = new Vector2(1.0f, 0.0f);
        sectionNormal[1] = new Vector2(0.0f, 1.0f);
        sectionNormal[2] = new Vector2(1.0f, 0.0f);
        sectionNormal[3] = new Vector2(0.0f, 1.0f);
        sectionNormal[4] = new Vector2(-1.0f, 0.0f);
        sectionNormal[5] = new Vector2(0.0f, -1.0f);
        sectionNormal[6] = new Vector2(-1.0f, 0.0f);
        sectionNormal[7] = new Vector2(0.0f, -1.0f);

        for (int i = 0; i < definition + 1; ++i)
        {
            float theta = ((float)i) * Mathf.PI * 2.0f / definition;
            float u = ((float)i) / definition;

            for (int j = 0; j < 16; ++j)
            {
                int idx = i * 16 + j;

                int posIdx = ((j + 1) % 16) / 2;
                int normalIdx = j / 2;
                float v = ((j + 1) / 2) / 8.0f;

                {
                    float r = sectionPos[posIdx].x;
                    float l = sectionPos[posIdx].y;
                    positions[idx] = new Vector3(r * Mathf.Cos(theta), l, r * Mathf.Sin(theta));
                }

                {
                    float r = sectionNormal[normalIdx].x;
                    float l = sectionNormal[normalIdx].y;
                    normals[idx] = new Vector3(r * Mathf.Cos(theta), l, r * Mathf.Sin(theta));
                }

                uvs[idx] = new Vector2(u, v);
                colors[idx] = Color.white;
            }
        }

        // indices
        int[] indices = new int[definition * 8 * 6];
        for (int i = 0; i < definition; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                int baseIdx = (i * 8 + j) * 6;

                int baseVert1 = i * 16 + j * 2;
                int baseVert2 = baseVert1 + 16;

                indices[baseIdx + 0] = baseVert1;
                indices[baseIdx + 1] = baseVert1 + 1;
                indices[baseIdx + 2] = baseVert2;
                indices[baseIdx + 3] = baseVert2 + 1;
                indices[baseIdx + 4] = baseVert2;
                indices[baseIdx + 5] = baseVert1 + 1;
            }
        }

        mesh.vertices = positions;
        mesh.normals = normals;
        mesh.colors = colors;
        mesh.uv = uvs;
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);

        return mesh;
    }
}