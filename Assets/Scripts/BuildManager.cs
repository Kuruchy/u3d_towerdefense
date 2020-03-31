using System;
using UnityEngine;

public class BuildManager : MonoBehaviour {
    // To be sure there is only one instance
    public static BuildManager instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildManager");
            return;
        }

        instance = this;
    }

    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild {
        get { return turretToBuild != null; }
    }

    public bool HasMoney {
        get { return PlayerStats.Money >= turretToBuild.cost; }
    }

    public void SelectNode(Node node) {
        if (selectedNode == node) {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode() {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turretBlueprint) {
        turretToBuild = turretBlueprint;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild() {
        return turretToBuild;
    }
}