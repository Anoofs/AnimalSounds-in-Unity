using UnityEngine;
using System.Collections;

public class AddButton : MonoBehaviour {

    [SerializeField]
    private Transform gameField;

    [SerializeField]
    private GameObject leftButton;

    [SerializeField]
    private GameObject rightButton;

    [SerializeField]
    private GameObject soundButton;

    void Awake()
    {
        GameObject leftAnimal = Instantiate(leftButton);
        leftAnimal.name = "leftAnimal";
        leftAnimal.transform.SetParent(gameField, false);

        GameObject rightAnimal = Instantiate(rightButton);
        rightAnimal.name = "rightAnimal";
        rightAnimal.transform.SetParent(gameField, false);

        GameObject playSoundButton = Instantiate(soundButton);
        playSoundButton.name = "playSound";
        playSoundButton.transform.SetParent(gameField, false);
    }
}
