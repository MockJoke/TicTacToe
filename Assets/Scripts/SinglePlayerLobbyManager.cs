using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerLobbyManager : MonoBehaviour
{
    public void SelectGameMode(int no)
    {
        switch (no)
        {
            case 0:
                SceneManager.LoadScene(sceneBuildIndex: 2);
                break;
            case 1:
                SceneManager.LoadScene(sceneBuildIndex: 3);
                break;
        }
    }
}
