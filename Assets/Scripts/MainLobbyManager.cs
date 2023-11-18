using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobbyManager : MonoBehaviour
{
    public void SelectGameMode(int no)
    {
        switch (no)
        {
            case 0:
                SceneManager.LoadScene(sceneBuildIndex: 1);
                break;
            case 1:
                SceneManager.LoadScene(sceneBuildIndex: 4);
                break;
        }
    }
}
