using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ArduinoCommunicatorScript : MonoBehaviour
{

    void Awake()
    {
        //Verifier si il n'y a pas d'autres instances
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        //Initialiser la communication avec arduino
        arduino = new SerialPort("COM6", 9600);
        arduino.ReadTimeout = 50;
        arduino.Open();

    }

    //Désencapsuler le message
    private string decodeMessage(string msg)
    {
        string toReturn = null;

        //Verifier que la trame est entière
        if(msg.Contains("!") && msg.Contains("?"))
        {
            toReturn = msg.Replace('!', ' ');
            toReturn = toReturn.Replace('?', ' ');
            toReturn = toReturn.Trim();
        }

        return toReturn;
    }


    //Effacer le flag
    public void clearFlag()
    {
        this.messageReceivedFlag = false;
    }

    //Lancer la lecture d'un message
    public void startArduinoRead()
    {
        StartCoroutine(readFromArduino());
    }

    //Coroutine pour attendre un message de l'arduino
    private IEnumerator readFromArduino()
    {

        string dataString = null;

        do
        {
            try
            {
                dataString = arduino.ReadLine();
            }
            catch (System.TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                //Decoder le mesage recu
                dataString = decodeMessage(dataString);
                if(dataString != null)
                {
                    messageReceivedFlag = true;
                    messageReceived = dataString;
                    yield break;
                }
            }
            else
                yield return null;

        } while (dataString == null);
        yield return null;

    }

    //Retourne si un message est recu
    public bool getIsMessageReceived()
    {
        return this.messageReceivedFlag;
    }

    //Retourne le message
    public string getMessage()
    {
        return this.messageReceived;
    }

    //Variables
    SerialPort arduino;
    private bool messageReceivedFlag = false;
    private string messageReceived = "";

    //Instance du script
    public static ArduinoCommunicatorScript instance = null; 
}
