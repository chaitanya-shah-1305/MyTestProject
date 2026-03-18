using UnityEngine;

namespace _Project.Scripts
{
    public class GridSelection : MonoBehaviour
    {
        public void OnGridSelection(int gridId)
        {
            var row = 2;
            var col = 2;
            switch (gridId)
            {
                case 2:
                    row = 3;
                    col = 4;
                    break;
                case 3:
                    row = 4;
                    col = 3;
                    break;
                case 4:
                    row = 4;
                    col = 4;
                    break;
                case 5:
                    row = 4;
                    col = 5;
                    break;
                case 6:
                    row = 5;
                    col = 4;
                    break;
                case 7:
                    row = 4;
                    col = 6;
                    break;
                case 8:
                    row = 6;
                    col = 4;
                    break;
                case 9:
                    row = 5;
                    col = 6;
                    break;
                case 10:
                    row = 6;
                    col = 5;
                    break;
            }

            UIController.Instance.StartGameFromGridSelection(row, col);
        }
    }
}