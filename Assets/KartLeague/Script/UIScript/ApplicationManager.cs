using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class ApplicationManager : MonoBehaviour {

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
    public void OnClickPlay()
    {
        if (PhotonNetwork.InRoom)
        {
			PhotonNetwork.LeaveRoom();
		}
		SceneManager.LoadScene(1);
	}
}
