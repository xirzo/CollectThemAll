using UnityEngine;
using UnityEngine.SceneManagement;

namespace Collect.Domain.Scenes
{
	public class NextSceneLoader : MonoBehaviour
	{
		public void LoadNextScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}