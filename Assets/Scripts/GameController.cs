using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {

    private int TOTAL_BUTTONS = 2;

    private Button soundButton;
    private int correctButtonIndex;
    private string correctAnimalName;
    private AudioSource source;
    private AudioClip correctAnimalSound;
    public Button[] buttons;
    public Sprite[] animals;
    public AudioClip[] sounds;
    public List<Sprite> gameAnimals = new List<Sprite>();

    private Text textField;

    void Awake()
    {
        animals = Resources.LoadAll<Sprite>("Sprites/Animals");
        sounds = Resources.LoadAll<AudioClip>("Sounds");
    }

	void Start () 
    {
        GetButtons();
        AddListeners();
        Reload();
	}

    void Reload()
    {
        destroySound();
        AddAnimals();
        AddSound();
        Invoke("clearText", 2);
    }

    void clearText()
    { this.textField.text = "";
    }

    void AddAnimals()
    {
        int randomAnimalIndex = Random.Range(0, animals.Length);
        this.correctAnimalName = animals[randomAnimalIndex].name;

        //add correct animal sprite to the randomly generated correct button
        int randomButtonIndex = Random.Range(0, buttons.Length);
        correctButtonIndex = randomButtonIndex;
        buttons[correctButtonIndex].image.sprite = animals[randomAnimalIndex];

        //add wrong animals to the remaining buttons
        for (int i = 0; i < buttons.Length; ++i)
        {
            if (i != correctButtonIndex)
            {
                int incorrectAnimalIndex;
                do{incorrectAnimalIndex = Random.Range(0, animals.Length);
                } while (randomAnimalIndex == incorrectAnimalIndex);

                buttons[i].image.sprite = animals[incorrectAnimalIndex];
            }
        }
    }

    void AddSound()
    {
        //assign the relevant sound to sound button
        foreach(AudioClip snd in sounds){
            if (snd.name == correctAnimalName + "_sound")
            {
                correctAnimalSound = snd;
            }
        }
    }

    void GetButtons()
    {
        buttons = new Button[this.TOTAL_BUTTONS];

        GameObject leftButton = GameObject.Find("leftAnimal");
        buttons[0] = leftButton.GetComponent<Button>();
        GameObject rightButton = GameObject.Find("rightAnimal");
        buttons[1] = rightButton.GetComponent<Button>();

        GameObject soundButtonObject = GameObject.Find("playSound");
        this.soundButton = soundButtonObject.GetComponent<Button>();
        this.soundButton.onClick.AddListener(() => playSound());

        GameObject textObject = GameObject.Find("textField");
        this.textField = textObject.GetComponent<Text>();
        clearText();
    }

    void AddListeners()
    {
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => clickedButton());
        }
    }

    void clickedButton()
    {
        //check for correct answer and respond accordingly and reload if correct
        string btnName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if (btnName == buttons[correctButtonIndex].name)
        {
           // Debug.Log("You clicked the correct animal");
            this.textField.text = "You clicked the correct animal";
            Reload();
        }
        else
        {
           // Debug.Log("You clicked the wrong animal");
            this.textField.text = "You clicked the wrong animal";
        }

    }

    void playSound()
    {
        destroySound();
        source = gameObject.AddComponent<AudioSource>();
        source.clip = this.correctAnimalSound;
        source.Play();
    }

    void destroySound(){
        if (source)
            Destroy(source);
    }
	
}
