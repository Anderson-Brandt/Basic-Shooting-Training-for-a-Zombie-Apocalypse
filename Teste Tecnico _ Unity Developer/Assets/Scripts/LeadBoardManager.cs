using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    [Header("SheetDB Config")]
    public string sheetDbUrl = "https://sheetdb.io/api/v1/qid1u29g5u5sv"; // Cole o link da sua API aqui

    [Header("UI")]
    public TMP_InputField nameInputField;
    public TextMeshProUGUI leaderboardText;
    public int scoreToSubmit = 0;

    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int score;
    }

    [System.Serializable]
    public class Wrapper
    {
        public PlayerData[] data;
    }

    public void Update()
    {
        scoreToSubmit = GameManager.instance.score;
    }

    // Envia a pontuação
    public void SubmitScore()
    {
        string playerName = nameInputField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Nome do jogador está vazio.");
            return;
        }

        PlayerData data = new PlayerData
        {
            name = playerName,
            score = scoreToSubmit
        };

        Wrapper wrapper = new Wrapper
        {
            data = new PlayerData[] { data }
        };

        string json = JsonUtility.ToJson(wrapper);
        StartCoroutine(PostScore(json));
    }

    // Busca o ranking
    public void LoadLeaderboard()
    {
        StartCoroutine(GetScores());
    }

    IEnumerator PostScore(string jsonData)
    {
        UnityWebRequest request = new UnityWebRequest(sheetDbUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score enviado com sucesso!");
            LoadLeaderboard(); // Atualiza ranking após envio
        }
        else
        {
            Debug.LogError("Erro ao enviar score: " + request.error);
        }
    }

    IEnumerator GetScores()
    {
        UnityWebRequest request = UnityWebRequest.Get(sheetDbUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao buscar ranking: " + request.error);
        }
        else
        {
            string json = "{\"data\":" + request.downloadHandler.text + "}"; // Adiciona wrapper para desserializar
            Wrapper ranking = JsonUtility.FromJson<Wrapper>(json);

            var sortedRanking = ranking.data.OrderByDescending(p => p.score).ToList();

            leaderboardText.text = "\n";

            foreach (var player in sortedRanking)
            {
                leaderboardText.text += $"{player.name}: {player.score}\n";
            }
        }
    }
}



