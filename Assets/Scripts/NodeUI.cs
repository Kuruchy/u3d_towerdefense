using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject ui;

    public Text upgradeCost;

    private Node target;

    public Button upgradeButton;

    public void SetTarget(Node _target)
    {
        this.target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = this.target.turretBlueprint.upgradeCost + "€";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "UPGRADED!";
            upgradeButton.interactable = false;

        }

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
