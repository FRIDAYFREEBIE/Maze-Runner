using UnityEngine;

// 그리드 셀
public class Cell
{
  public int r { get; }
  public int c { get; }
  public Vector3 worldPos { get; }
  public bool visited { get; private set; }

  // 인접한 벽 (초기에는 모두 활성화)
  public bool north { get; set; } = true;
  public bool south { get; set; } = true;
  public bool east  { get; set; } = true;
  public bool west  { get; set; } = true;

  // 생성자
  public Cell(int row, int col, Vector3 pos)
  {
    r = row;
    c = col;
    worldPos = pos;
    visited = false;
  }

  public void Visit() => visited = true;
}