using UnityEngine;

// GridGenerator 디버거 코드는 AI를 사용해서 한번에 구현했다.
// AI를 사용하니 확실히 작업 속도가 눈에 띄게 빨라졌다.
// 잡다한 코드는 작업 효율을 생각해보면 AI에게 맡기는게 더 나은 것 같다.

[ExecuteAlways]
public class GridGeneratorDebugger : MonoBehaviour
{
  [Header("Target")]
  public GridGenerator targetGen;

  [Header("Toggles")]
  public bool drawGridLines = true;
  public bool drawCellCenters = true;

  [Header("Style")]
  public Color gridColor = new Color(0f, 1f, 0f, 1f);
  public Color centerColor = Color.red;
  [Min(0f)] public float centerRadius = 0.05f;

  void OnDrawGizmos()
  {
    if(targetGen == null) return;

    int rows = targetGen.Rows;
    int cols = targetGen.Cols;
    float cellSize = targetGen.CellSize;
    Vector3 origin = targetGen.OriginPos;

    if(rows <= 0 || cols <= 0 || cellSize <= 0f) return;

    float width = cols * cellSize;
    float height = rows * cellSize;

    Vector3 min = origin - new Vector3(width, 0f, height) * 0.5f;

    if(drawGridLines)
    {
      Gizmos.color = gridColor;

      for(int r = 0; r <= rows; r++)
      {
        Vector3 a = min + new Vector3(0f, 0f, r * cellSize);
        Vector3 b = min + new Vector3(width, 0f, r * cellSize);
        Gizmos.DrawLine(a, b);
      }

      for(int c = 0; c <= cols; c++)
      {
        Vector3 a = min + new Vector3(c * cellSize, 0f, 0f);
        Vector3 b = min + new Vector3(c * cellSize, 0f, height);
        Gizmos.DrawLine(a, b);
      }
    }

    if(drawCellCenters && targetGen.Cells != null)
    {
      Gizmos.color = centerColor;
      foreach(var cell in targetGen.Cells)
      {
        if(cell == null) continue;
        Gizmos.DrawSphere(cell.worldPos, Mathf.Max(0f, centerRadius));
      }
    }
  }
}