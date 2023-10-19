using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    void Awake()
    {
        if (gameController == null)
            gameController = FindObjectOfType<GameController>();
    }

    void Start()
    {
        gameController.IsAI = true;
    }
}
