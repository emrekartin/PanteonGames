using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RankSystem : MonoBehaviour
{
    public static RankSystem rankSystem;
    public GameObject player;
    public GameObject[] players;
    GameObject _temp;
    public int rankForPlayer;
    public int t = 0;
    public GameObject textView;
    public bool LosePlayer;
    private void Awake()
    {
        rankSystem = this;

    }
    private void Start()
    {
        player = Player.player.transform.gameObject;
        players[0] = Player.player.transform.gameObject;
    }
    void Update()
    {
        Rank();
        PlayerRank();
        WriteText();
    }
    void Rank()
    {
        for (int i = t; i < players.Length - 1; i++)
        {
            for (int j = i; j < players.Length; j++)
            {
                if (Vector3.Distance(LevelManagement.levelManagement.finishLine.transform.position, players[i].transform.position) > Vector3.Distance(LevelManagement.levelManagement.finishLine.transform.position, players[j].transform.position))
                {
                    _temp = players[j];
                    players[j] = players[i];
                    players[i] = _temp;
                }
            }
        }
    }
    void PlayerRank()
    {
        rankForPlayer = System.Array.FindIndex(players, o => o == player); 
    }
    void WriteText()
    {
        textView.GetComponent<TextMeshProUGUI>().text = (rankForPlayer+1)+"/11";
    }
}
