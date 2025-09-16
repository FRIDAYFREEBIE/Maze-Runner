using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeVisualizer : MonoBehaviour
{
  [Header("References")]
  public GridGenerator gridGenerator;
  public GameObject wallPrefab;

  [Header("Size")]
  public float wallThickness = 0.1f;

  [Header("Animation")]
  public float totalBuildTime = 5f;

  private float cellSize = 1f;

  void Start()
  {
    cellSize = gridGenerator.CellSize;
  }

  public void StartBuildAnimation()
  {
    StartCoroutine(BuildMazeWallsStepByStep());
  }

  private IEnumerator BuildMazeWallsStepByStep()
  {
    int rows = gridGenerator.Rows;
    int cols = gridGenerator.Cols;
    float cellSize = gridGenerator.CellSize;

    Vector3 origin = gridGenerator.OriginPos;
    Vector3 min = origin - new Vector3(cols * cellSize, 0f, rows * cellSize) * 0.5f;

    List<(Vector3 pos, bool isHorizontal)> wallData = new();

    for (int r = 0; r < rows; r++)
    {
      for (int c = 0; c < cols; c++)
      {
        var cell = gridGenerator.Cells[r, c];
        Vector3 cellPos = min + new Vector3(c * cellSize, 0f, r * cellSize);

        if (cell.north) // 위쪽(가로)
        {
          Vector3 center = cellPos + new Vector3(cellSize * 0.5f, 0f, 0f);
          wallData.Add((center, true));
        }
        if (cell.west)  // 왼쪽(세로)
        {
          Vector3 center = cellPos + new Vector3(0f, 0f, cellSize * 0.5f);
          wallData.Add((center, false));
        }
        if (r == rows - 1 && cell.south) // 아래쪽(가로)
        {
          Vector3 center = cellPos + new Vector3(cellSize * 0.5f, 0f, cellSize);
          wallData.Add((center, true));
        }
        if (c == cols - 1 && cell.east)  // 오른쪽(세로)
        {
          Vector3 center = cellPos + new Vector3(cellSize, 0f, cellSize * 0.5f);
          wallData.Add((center, false));
        }
      }
    }

    float delay = totalBuildTime / Mathf.Max(1, wallData.Count);

    foreach (var w in wallData)
    {
      GameObject wall = Instantiate(wallPrefab, w.pos, Quaternion.identity, transform);

      if (w.isHorizontal)
        wall.transform.localScale = new Vector3(cellSize, wall.transform.localScale.y, wallThickness);
      else
        wall.transform.localScale = new Vector3(wallThickness, wall.transform.localScale.y, cellSize);

      yield return new WaitForSeconds(delay);
    }
  }
}

