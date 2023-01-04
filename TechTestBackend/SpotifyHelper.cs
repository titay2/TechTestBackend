using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace TechTestBackend;

public static class SpotifyHelper
{
    public static Soptifysong[] GetTracks(string name)
    {
        var client = new HttpClient();
        var c_id = "996d0037680544c987287a9b0470fdbb";
        var c_s = "5a3c92099a324b8f9e45d77e919fec13";
        var e = Encoding.ASCII.GetBytes($"{c_id}:{c_s}");
        var base64 = Convert.ToBase64String(e);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
        
        var password = client.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(new [] { new KeyValuePair<string, string>("grant_type", "client_credentials") })).Result;
        dynamic Password_content = JsonConvert.DeserializeObject(password.Content.ReadAsStringAsync().Result);
        
        client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Password_content.access_token.ToString());
        
        var response = client.GetAsync("https://api.spotify.com/v1/search?q=" + name + "&type=track").Result;
        dynamic objects = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

        var songs = JsonConvert.DeserializeObject<Soptifysong[]>(objects.tracks.items.ToString());
        
        return songs;
    }

    public static Soptifysong GetTrack(string id)
    {
        var client = new HttpClient();
        var c_id = "996d0037680544c987287a9b0470fdbb";
        var c_s = "5a3c92099a324b8f9e45d77e919fec13";
        var e = Encoding.ASCII.GetBytes($"{c_id}:{c_s}");
        var base64 = Convert.ToBase64String(e);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
        
        var password = client.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(new [] { new KeyValuePair<string, string>("grant_type", "client_credentials") })).Result;
        dynamic Password_content = JsonConvert.DeserializeObject(password.Content.ReadAsStringAsync().Result);
        
        client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Password_content.access_token.ToString());
        
        var response = client.GetAsync("https://api.spotify.com/v1/tracks/" + id + "/").Result;
        dynamic objects = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

        var song = JsonConvert.DeserializeObject<Soptifysong>(objects.ToString());
        
        return song;
    }
}