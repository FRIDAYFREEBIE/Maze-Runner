using UnityEngine;

// GridGenerator 디버거 코드는 AI를 사용해서 한번에 구현했다.
// AI를 사용하니 확실히 작업 속도가 눈에 띄게 빨라졌다.
// 잡다한 코드는 작업 효율을 생각해보면 AI에게 맡기는게 더 나은 것 같다.

public class GridGeneratorDebugger : MonoBehaviour
{
  [Header("Target")]
  public MazeGenerator targetGen;   // 🔹 MazeGenerator로 변경

  [Header("Toggles")]
  public bool drawMazeWalls = true;
  public bool drawCellCenters = true;

  [Header("Style")]
  public Color wallColor = Color.green;
  public Color centerColor = Color.red;
  [Min(0f)] public float centerRadius = 0.05f;

  void OnDrawGizmos()
  {
    if (targetGen == null) return;
    if (targetGen.gridGenerator == null) return;
    if (targetGen.gridGenerator.Cells == null) return; // 🔹 추가

    int rows = targetGen.gridGenerator.Rows;
    int cols = targetGen.gridGenerator.Cols;
    float cellSize = targetGen.gridGenerator.CellSize;
    Vector3 origin = targetGen.gridGenerator.OriginPos;

    if (rows <= 0 || cols <= 0 || cellSize <= 0f) return;

    float width = cols * cellSize;
    float height = rows * cellSize;

    Vector3 min = origin - new Vector3(width, 0f, height) * 0.5f;

    // 🔹 미로 벽 상태 표시
    if (drawMazeWalls)
    {
      Gizmos.color = wallColor;

      for (int r = 0; r < rows; r++)
      {
        for (int c = 0; c < cols; c++)
        {
          Cell cell = targetGen.gridGenerator.Cells[r, c];
          if (cell == null) continue;

          Vector3 cellPos = min + new Vector3(c * cellSize, 0f, r * cellSize);

          if (cell.north)
            Gizmos.DrawLine(cellPos, cellPos + new Vector3(cellSize, 0f, 0f));
          if (cell.west)
            Gizmos.DrawLine(cellPos, cellPos + new Vector3(0f, 0f, cellSize));
          if (r == rows - 1 && cell.south)
            Gizmos.DrawLine(cellPos + new Vector3(0f, 0f, cellSize),
                            cellPos + new Vector3(cellSize, 0f, cellSize));
          if (c == cols - 1 && cell.east)
            Gizmos.DrawLine(cellPos + new Vector3(cellSize, 0f, 0f),
                            cellPos + new Vector3(cellSize, 0f, cellSize));
        }
      }
    }

    if (drawCellCenters && targetGen.gridGenerator.Cells != null)
    {
      Gizmos.color = centerColor;
      foreach (var cell in targetGen.gridGenerator.Cells)
      {
        if (cell == null) continue;
        Gizmos.DrawSphere(cell.worldPos, Mathf.Max(0f, centerRadius));
      }
    }
  }
}
