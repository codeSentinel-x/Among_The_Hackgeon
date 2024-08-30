using TMPro;
using UnityEngine;

public class CommandConsole : MonoBehaviour {

    public static CommandConsole I;
    public TMP_InputField commandInput;

    private const string command_addItem = "additem";

    private void Awake() {
        I = this;
        commandInput.onSubmit.AddListener((string inpt) => RunCommand(inpt));
    }



    private void RunCommand(string input) {
        commandInput.text = "";
        string[] splittedInput = input.Split(" ");
        splittedInput[0] = splittedInput[0].ToLower();
        // Debug.Log(splittedInput[0] + splittedInput[1] + splittedInput[2]);

        switch (splittedInput[0]) {
            case command_addItem: {
                    Debug.Log("adding");
                    AddItem(splittedInput[1], splittedInput[2]);
                    break;
                }
            default: {
                    Debug.Log("No command found for+ " + splittedInput[0]);
                    break;
                }
        }
    }

    public void AddItem(string itemName, string amount) {
        // todo
    }

    public static void Show() {
        I.gameObject.SetActive(true);
    }
    public static void Hide() {
        I.commandInput.text = "";
        I.gameObject.SetActive(false);
    }


}
