using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// WARNING: THIS SCRIPT REQUIRES THE CANVAS RENDERER COMPONENET TO WORK

public class GridRenderer : Graphic
{
    // Rows and Columns in grid 
    public Vector2Int gridSize = new Vector2Int(1, 1);
    // Thickness of graph line
    public float thickness = 10f;

    public float width;
    public float height;
    float cellWidth;
    float cellHeight;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        // Clears Vertex cache so that we can input new vertices
        vh.Clear();

        // Gets height and width of parent class
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;


        cellWidth = width / (float)gridSize.x;
        cellHeight = height / (float)gridSize.y;

        int count = 0;

        for(int y = 0; y < gridSize.y; y++)
        {
            for(int x = 0; x < gridSize.x; x++)
            {
                DrawCell(x, y, count, vh);
                count++;
            }
        }

    }

    private void DrawCell(int x, int y, int index, VertexHelper vh)
    {

        float xPos = cellWidth * x;
        float yPos = cellHeight * y;

        // Uhh idk
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        // This will add 4 vertices to the vertex cache.
        // These vertices will be the outside corners of the square

        // Adds a vertex at the specifed Vector3
        vertex.position = new Vector3(xPos, yPos);
        vh.AddVert(vertex);

        // Adds a vertex at the specifed Vector3
        vertex.position = new Vector3(xPos, yPos + cellHeight);
        vh.AddVert(vertex);

        // Adds a vertex at the specifed Vector3
        vertex.position = new Vector3(xPos + cellWidth, yPos + cellHeight);
        vh.AddVert(vertex);

        // Adds a vertex at the specifed Vector3
        vertex.position = new Vector3(xPos + cellWidth, yPos);
        vh.AddVert(vertex);

        // vh.AddTriangle(0, 1, 2);
        // vh.AddTriangle(2, 3, 0);

        // I have no idea what this is for
        float widthSqr = thickness * thickness;
        float distanceSqr = widthSqr / 2f;
        float distance = Mathf.Sqrt(distanceSqr);

        // This will add 4 vertices to the vertex cache.
        // These vertices will be the inside corners of the square

        // Adds a vertex at the specifed Vector3
        vertex.position = new Vector3(xPos + distance, yPos + distance);
        vh.AddVert(vertex);

        // Adds a vertex at the specifed Vector3
        vertex.position = new Vector3(xPos + distance, yPos + (cellHeight - distance));
        vh.AddVert(vertex);

        // Adds a vertex at the specifed Vector3
        vertex.position = new Vector3(xPos + (cellWidth - distance), yPos + (cellHeight - distance));
        vh.AddVert(vertex);

        // Adds a vertex at the specifed Vector3
        vertex.position = new Vector3(xPos + (cellWidth - distance), yPos + distance);
        vh.AddVert(vertex);

        int offset = index * 8;

        // This will draw the actually square
        // Here is how the vertices are arranged
        //   1--------2
        //   | 5----6 |
        //   | |    | |
        //   | 4----7 |
        //   0--------3
        // It really easy to visualize now
        // Left Edge
        vh.AddTriangle(offset + 0, offset + 1, offset + 5);
        vh.AddTriangle(offset + 5, offset + 4, offset + 0);

        // Top Edge
        vh.AddTriangle(offset + 1, offset + 2, offset + 6);
        vh.AddTriangle(offset + 6, offset + 5, offset + 1);

        // Right Edge
        vh.AddTriangle(offset + 2, offset + 3, offset + 7);
        vh.AddTriangle(offset + 7, offset + 6, offset + 2);

        // Bottom Edge
        vh.AddTriangle(offset + 3, offset + 0, offset + 4);
        vh.AddTriangle(offset + 4, offset + 7, offset + 3);

    }

}
