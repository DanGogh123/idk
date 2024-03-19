using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System.IO;
using UnityEngine.UI;
using TMPro;



public class control : MonoBehaviour
{
    //[MenuItem("Tools/Write file")]
    static void WriteString(string s)
    {
        string path = "C:/Users/octav/Desktop/Out.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(s);
        writer.Close();
    }

    //=====================
    public float Sensitivity {
		get { return sensitivity; }
		set { sensitivity = value; }
	}
    public float speed = 3f;

	[Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
	[Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
	[Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

	Vector2 rotation = Vector2.zero;
	const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage
	const string yAxis = "Mouse Y";
    int i = 0;
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public TMP_Text text;
    bool recording = false;
	void Update(){

        rotation.x += Input.GetAxis(xAxis) * sensitivity;
		rotation.y += Input.GetAxis(yAxis) * sensitivity;
		rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        Vector3 translation = new Vector3(Input.GetAxis("Horizontal"),
        Input.GetAxis("Bruh"),
        Input.GetAxis("Vertical"));
		var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
		var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);
        transform.position += transform.TransformVector(translation * speed * Time.deltaTime);
		transform.localRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
        string s = i.ToString() + " " + transform.position.x.ToString() + " "
                                      + transform.position.y.ToString() + " "
                                      + transform.position.z.ToString() + " "
                                      + transform.rotation.x.ToString() + " "
                                      + transform.rotation.y.ToString() + " "
                                      + transform.rotation.z.ToString();
        if(recording){
        WriteString(s);i++;
        text.text = i.ToString();
        }else{
            text.text = "-";
        }
        if(Input.GetKeyDown(KeyCode.R)){
            recording = !recording;
        }
	}
}
