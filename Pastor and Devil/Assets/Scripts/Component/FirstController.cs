using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstController : MonoBehaviour, SceneController, UserAction {
    ShoreCtrl leftShoreController, rightShoreController;
    River river;
    BoatCtrl boatController;
    RoleCtrl[] roleControllers;
    MoveCtrl moveController;
    bool isRunning;
    float time;

    public void LoadResources() {
        //role
        roleControllers = new RoleCtrl[6];
        for (int i = 0; i < 6; ++i) {
            roleControllers[i] = new RoleCtrl();
            roleControllers[i].CreateRole(Position.role_shore[i], i < 3 ? true : false, i);
        }

        // 加载此岸和彼岸
        leftShoreController = new ShoreCtrl();
        leftShoreController.CreateShore(Position.left_shore);
        leftShoreController.GetShore().shore.name = "this_shore";
        rightShoreController = new ShoreCtrl();
        rightShoreController.CreateShore(Position.right_shore);
        rightShoreController.GetShore().shore.name = "other_shore";

        // 将人物添加并定位至左边 
        foreach (RoleCtrl roleController in roleControllers)
        {
            roleController.GetRoleModel().role.transform.localPosition = leftShoreController.AddRole(roleController.GetRoleModel());
        }
        //boat
        boatController = new BoatCtrl();
        boatController.CreateBoat(Position.left_boat);

        //river
        river = new River(Position.river);

        //move
        moveController = new MoveCtrl();

        isRunning = true;
        time = 60;
    }

    public void MoveBoat() {
        if (isRunning == false || moveController.GetIsMoving()) return;
        if (boatController.GetBoatModel().isRight) {
            moveController.SetMove(Position.left_boat, boatController.GetBoatModel().boat);
        }
        else {
            moveController.SetMove(Position.right_boat, boatController.GetBoatModel().boat);
        }
        boatController.GetBoatModel().isRight = !boatController.GetBoatModel().isRight;
    }

    public void MoveRole(Role roleModel) {
        if (isRunning == false || moveController.GetIsMoving()) return;
        if (roleModel.inBoat) {
            if (boatController.GetBoatModel().isRight) {
                moveController.SetMove(rightShoreController.AddRole(roleModel), roleModel.role);
            }
            else {
                moveController.SetMove(leftShoreController.AddRole(roleModel), roleModel.role);
            }
            roleModel.onRight = boatController.GetBoatModel().isRight;
            boatController.RemoveRole(roleModel);
        }
        else {
            if (boatController.GetBoatModel().isRight == roleModel.onRight) {
                if (roleModel.onRight) {
                    rightShoreController.RemoveRole(roleModel);
                }
                else {
                    leftShoreController.RemoveRole(roleModel);
                }
                moveController.SetMove(boatController.AddRole(roleModel), roleModel.role);
            }
        }
    }

    public void Check() {
        if (isRunning == false)
            return;
        this.gameObject.GetComponent<UserGUI>().gameMessage = "";
        if (rightShoreController.GetShore().pastorCount == 3) {
            this.gameObject.GetComponent<UserGUI>().gameMessage = "You Win!";
            isRunning = false;
        }
        else {
            int leftPastorCount, rightPastorCount, leftDevilCount, rightDevilCount;
            leftPastorCount = leftShoreController.GetShore().pastorCount + (boatController.GetBoatModel().isRight ? 0 : boatController.GetBoatModel().pastorCount);
            rightPastorCount = rightShoreController.GetShore().pastorCount + (boatController.GetBoatModel().isRight ? boatController.GetBoatModel().pastorCount : 0);
            leftDevilCount = leftShoreController.GetShore().devilCount + (boatController.GetBoatModel().isRight ? 0 : boatController.GetBoatModel().devilCount);
            rightDevilCount = rightShoreController.GetShore().devilCount + (boatController.GetBoatModel().isRight ? boatController.GetBoatModel().devilCount : 0);
            if (leftPastorCount != 0 && leftPastorCount < leftDevilCount || rightPastorCount != 0 && rightPastorCount < rightDevilCount) {
                this.gameObject.GetComponent<UserGUI>().gameMessage = "Game Over!";
                isRunning = false;
            }
        }
    }

    public void RestartGame(){
        if (GUI.Button(new Rect(0, 35, 100, 30), "Restart")){
            // 重新加载游戏场景，只有一个场景，那编号就是0
            SceneManager.LoadScene(0);
        }
    }

    void Awake() {
        Director.GetInstance().CurrentSceneController = this;
        LoadResources();
        this.gameObject.AddComponent<UserGUI>();
    }

    // Update is called once per frame
    void Update() {
        if (isRunning) {
            time -= Time.deltaTime;
            this.gameObject.GetComponent<UserGUI>().time = (int)time;
            if (time <= 0) {
                this.gameObject.GetComponent<UserGUI>().time = 0;
                this.gameObject.GetComponent<UserGUI>().gameMessage = "Game Over!";
                isRunning = false;
            }
        }
    }
}
