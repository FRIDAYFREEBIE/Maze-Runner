using UnityEngine;

public class MazeGenerator : MazeBuilder
{
  [Header("Generation Control")]
  [SerializeField] private bool generateOnStart = true;

  void Awake()
  {
    if (generateOnStart)
    {
      gridGenerator.CreateGrid();
      Prim();
    }
  }
}
