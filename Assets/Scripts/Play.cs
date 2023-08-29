using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    Text BtnText;
    GameObject btnHolder;
    Button btn; 
    string PlayerSide = "";
    //private GameController gameController;
    string PlayerTurn = "0";
    string[] BtnListTxt;
    string Winner; 
    public GameObject GameOverPanel;
    public Text GameOverText;
    public int MoveCount; 

    // Start is called before the first frame update
    void Start()
    {
        BtnListTxt = new string[9];
        //pv = GetComponent<PhotonView>();
        btnHolder = GameObject.Find("BtnHolder");
        BtnText = GameObject.Find("UserSide").GetComponent<Text>();
        //BtnText.text = PhotonNetwork.NickName;
        //PlayerSide = PhotonNetwork.NickName;
        //MoveCount = 0; 
    }

    public void OnBtnClick(int index)
    {
        string t = "";

        MoveCount++; 

        if (PlayerSide == "0")
        {
            t = "0";
            PlayerTurn = "X";
            btnHolder.transform.GetChild(index).GetComponentInChildren<Text>().text = "0";
        }
        else
        {
            t = "X";
            PlayerTurn = "0";
            btnHolder.transform.GetChild(index).GetComponentInChildren<Text>().text = "X";
        }
        BtnListTxt[index] = t;
        //gameController.EndTurn();

        //pv.RPC("syncData", RpcTarget.All, index, t, PlayerTurn,Winner);

        EndGame(); 
    }

    public void EndGame()
    {
       
        if (BtnListTxt[0]!="")
        {
            if (BtnListTxt[0] == BtnListTxt[1] && BtnListTxt[0] == BtnListTxt[2])
            {
                Winner = BtnListTxt[0];
                GameOver();
            }
            if (BtnListTxt[0] == BtnListTxt[3] && BtnListTxt[0] == BtnListTxt[6])
            {
                Winner = BtnListTxt[0];
                GameOver();
            }
            if (BtnListTxt[0] == BtnListTxt[4] && BtnListTxt[0] == BtnListTxt[8])
            {
                Winner = BtnListTxt[0];
                GameOver();
            }
        }

        if (BtnListTxt[3] != "")
        {
            if (BtnListTxt[3] == BtnListTxt[4] && BtnListTxt[3] == BtnListTxt[5])
            {
                Winner = BtnListTxt[3];
                GameOver();
            }
        }
        if (BtnListTxt[6] != "")
        {
            if (BtnListTxt[6] == BtnListTxt[7] && BtnListTxt[6] == BtnListTxt[8])
            {
                Winner = BtnListTxt[6];
                GameOver();
            }
        }


        if (BtnListTxt[1] != "")
        {
            if (BtnListTxt[1] == BtnListTxt[4] && BtnListTxt[1] == BtnListTxt[7])
            {
                Winner = BtnListTxt[1];
                GameOver();
            }
        }
        if (BtnListTxt[2] != "")
        {
            if (BtnListTxt[2] == BtnListTxt[5] && BtnListTxt[2] == BtnListTxt[8])
            {
                Winner = BtnListTxt[2];
                GameOver();
            }
            if (BtnListTxt[2] == BtnListTxt[4] && BtnListTxt[2] == BtnListTxt[6])
            {
                Winner = BtnListTxt[2];
                GameOver();
            }
        }
                
        if (MoveCount >= 9)
        {
            SetGameOverText("It's a draw!");
        }
    }

    void GameOver()
    {
        for (int i = 0; i < BtnListTxt.Length; i++)
        {
            //btnHolder.GetComponentInParent<Button>().interactable = false;
        }
        if (Winner!="")
        {
            SetGameOverText(Winner + " Wins!");
        }
        
    }

    void SetGameOverText(string value)
    {
        GameOverPanel.SetActive(true);
        GameOverText.text = value;
    }

    //[PunRPC]
    void syncData(int n, string txt, string turn,string w)
    {
        Winner = w;
        BtnListTxt[n] = txt;
        this.PlayerTurn = turn;
        btnHolder.transform.GetChild(n).GetComponentInChildren<Text>().text = txt;
    }

    //public void SetGameControllerReference(GameController controller)
    //{
    //    gameController = controller;
    //}

    // Update is called once per frame
    void Update()
    {
        if (PlayerTurn.Equals(PlayerSide))
        {
            GameObject.Find("BtnHolder").GetComponent<CanvasGroup>().interactable = true;
        }
        else
        {
            GameObject.Find("BtnHolder").GetComponent<CanvasGroup>().interactable = false;
        }
    }
}
