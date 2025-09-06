using UnityEngine;

// 그리드 셀
public class Cell
{
  public int r { get; }
  public int c { get; }
  public Vector3 worldPos { get; }
  public bool visited { get; private set; }

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