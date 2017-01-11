using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Money;
    public int startMoney = 150;

    public static int Life;
    public int startingLife = 20;

    public static int Rounds;

    private void Start()
    {
        Rounds = 0;
        Money = startMoney;
        Life = startingLife;
    }
}
