using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerParty : MonoBehaviour
{
    [SerializeField]List<DragonQuestPlayer> DPlayers;

    public List<DragonQuestPlayer> DPlayer;
    public int playerIndex;


    private void Start()
    {
        foreach (var DPlayer in DPlayers)
        {
            DPlayer.Init();
        }
    }

    public DragonQuestPlayer GetLivingPlayers()
    {
        return DPlayers.Where(x => x.Hp > 0).FirstOrDefault();
    }
}
