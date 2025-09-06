using UnityEngine;

public class GridGenerator : MonoBehaviour
{
  [SerializeField, Min(1)] private int rows = 10;
  [SerializeField, Min(1)] private int cols = 10;
  [SerializeField, Min(0.1f)] private float cellSize = 1f;
  [SerializeField] private Vector3 originPos = Vector3.zero;

  private Cell[,] cells;

  // 읽기 전용 프로퍼티
  public int Rows => rows;
  public int Cols => cols;
  public float CellSize => cellSize;
  public Vector3 OriginPos => originPos;
  public Cell[,] Cells => cells;

  // 그리드 생성
  public void CreateGrid()
  {
    float width = cols * cellSize;    // 가로 (X)
    float height = rows * cellSize;   // 새로 (Z)

    // 그리드 기준점
    Vector3 baseOriginPos = originPos - new Vector3(width, 0, height) * 0.5f + new Vector3(cellSize * 0.5f, 0, cellSize * 0.5f);

    // 2차원 배열
    cells = new Cell[rows, cols];

    // 셀 생성
    for(int r = 0; r < rows; r++)
    {
      for(int c = 0; c < cols; c++)
      {
        Vector3 pos = baseOriginPos + new Vector3(c * cellSize, 0, r * cellSize);
        Cell cell = new Cell(r, c, pos);

        cells[r, c] = cell;
      }
    }
  }
}
