using UnityEngine;

public class CellNode : MonoBehaviour
{
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject upWall;
    [SerializeField] private GameObject downWall;

    public bool isVisited { get; private set; }

    public void Visit()
    {
        isVisited = true;
    }

    public void deactivateLeftWall()
    {
        leftWall.SetActive(false);
    }

    public void deactivateRightWall()
    {
        rightWall.SetActive(false);
    }

    public void deactivateUpWall()
    {
        upWall.SetActive(false);
    }

    public void deactivateDownWall()
    {
        downWall.SetActive(false);
    }
}
