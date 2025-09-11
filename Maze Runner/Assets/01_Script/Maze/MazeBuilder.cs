using System.Collections.Generic;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
  // 그리드 생성기
  [Header("GridGenerator")]
  public GridGenerator gridGenerator;
  // 입구 좌표
  [Header("Entrance location")]
  [SerializeField] private int inRow = 0;
  [SerializeField] private int inCol = 0;

  // 출구 좌표
  [Header("Exit location")]
  [SerializeField] private int outRow = 14;
  [SerializeField] private int outCol = 14;

  // 입구 셀의 벽을 엶
  protected void OpenEntranceAndExit()
  {
    // 입구
    if (inRow == 0) gridGenerator.Cells[inRow, inCol].north = false;
    else if (inRow == gridGenerator.Rows - 1) gridGenerator.Cells[inRow, inCol].south = false;
    else if (inCol == 0) gridGenerator.Cells[inRow, inCol].west = false;
    else if (inCol == gridGenerator.Cols - 1) gridGenerator.Cells[inRow, inCol].east = false;

    // 출구
    if (outRow == 0) gridGenerator.Cells[outRow, outCol].north = false;
    else if (outRow == gridGenerator.Rows - 1) gridGenerator.Cells[outRow, outCol].south = false;
    else if (outCol == 0) gridGenerator.Cells[outRow, outCol].west = false;
    else if (outCol == gridGenerator.Cols - 1) gridGenerator.Cells[outRow, outCol].east = false;
  }

  // 셀에서 열 벽(미방문 셀로 가는 길)을 반환
  protected List<(int fromR, int fromC, int toR, int toC)> GetCandidateWalls(int row, int col)
  {
    List<(int, int, int, int)> candidates = new();

    // 위쪽
    if (row > 0 && !gridGenerator.Cells[row - 1, col].visited)
      candidates.Add((row, col, row - 1, col));

    // 아래쪽
    if (row < gridGenerator.Rows - 1 && !gridGenerator.Cells[row + 1, col].visited)
      candidates.Add((row, col, row + 1, col));

    // 오른쪽
    if (col < gridGenerator.Cols - 1 && !gridGenerator.Cells[row, col + 1].visited)
      candidates.Add((row, col, row, col + 1));

    // 왼쪽
    if (col > 0 && !gridGenerator.Cells[row, col - 1].visited)
      candidates.Add((row, col, row, col - 1));

    return candidates;
  }

  // 두 셀 사이의 벽을 제거
  protected void RemoveWallBetween(Cell a, Cell b)
  {
    // 같은 행 일 때
    if (a.r == b.r)
    {
      if (a.c == b.c + 1) { a.west = false; b.east = false; }
      else if (a.c == b.c - 1) { a.east = false; b.west = false; }
    }

    // 같은 열 일때
    else if (a.c == b.c)
    {
      if (a.r == b.r + 1) { a.north = false; b.south = false; }
      else if (a.r == b.r - 1) { a.south = false; b.north = false; }
    }
  }

  // 프림 알고리즘으로 미로 생성
  protected void Prim()
  {
    // 입구와 출구를 열어놓음
    OpenEntranceAndExit();

    // 입구 방문 표시
    Cell startCell = gridGenerator.Cells[inCol, inRow];
    startCell.Visit();

    // 인접 벽 리스트
    List<(int fr, int fc, int tr, int tc)> frontier = new();
    frontier.AddRange(GetCandidateWalls(inRow, inCol));

    // 미로 생성
    while (frontier.Count > 0)
    {
      // 무작위 선택
      int index = Random.Range(0, frontier.Count);
      var edge = frontier[index];
      frontier.RemoveAt(index);

      Cell fromCell = gridGenerator.Cells[edge.fr, edge.fc];
      Cell toCell = gridGenerator.Cells[edge.tr, edge.tc];

      if (toCell.visited) continue;

      // 벽 제거
      RemoveWallBetween(fromCell, toCell);

      // 방문 처리
      toCell.Visit();

      // 새로 방문한 셀에서 나온 벽들을 추가
      frontier.AddRange(GetCandidateWalls(toCell.r, toCell.c));
    }
  }
}