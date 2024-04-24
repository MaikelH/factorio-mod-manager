using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using FactorioLib.Types;

namespace FactorioLib;

/// <summary>
/// The ModPortal class is responsible for interacting with the Factorio mod portal API.
/// It provides functionality to retrieve mod information using various parameters.
///
/// See https://wiki.factorio.com/Mod_portal_API for more information.
/// </summary>
/// 
public class ModPortal
{
    private readonly Uri _baseUrl = new("https://mods.factorio.com/api/mods");
    private readonly HttpClient _httpClient;
    private string _token;
    private string _username;

    public ModPortal()
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = _baseUrl
        };
    }

    public async Task<ModListResponse> GetMods(ModsRequestParameters parameters)
    {
        var uriBuilder = new UriBuilder(_baseUrl);
        var queryString = parameters.GetQueryString();
        uriBuilder.Query = queryString;

        var response = await _httpClient.GetFromJsonAsync<ModListResponse>(uriBuilder.ToString());
        if (response != null)
        {
            return response;
        } 

        throw new Exception("response is null or invalid");
    }
    
    public async Task Download(string modName, string? version, string modDirectory)
    {
        var modInfo = await GetMod(modName);
        
        // If version is not specified, download the latest version
        if (string.IsNullOrEmpty(version))
        {
            var release = modInfo.Releases.Last();
            if (release == null)
            {
                throw new Exception($"Download URL is null for {modName} release {modInfo.LatestRelease?.Version}");
            }
            var uriBuilder = new UriBuilder($"https://mods.factorio.com{release.DownloadUrl}")
            {
                Query = $"username={_username}&token={_token}"
            };

            try
            {
                var response = await _httpClient.GetAsync(uriBuilder.Uri.ToString());
                response.EnsureSuccessStatusCode();
            
                var modfilePath = modDirectory + Path.DirectorySeparatorChar + release.FileName;
                using (var fileStream = new FileStream(modfilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fileStream);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to download mod {modName} - {e.Message}");    
            }
        }
    }
    
    public async Task<ModPortalMod> GetMod(string modName)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/{modName}");
        if (response.IsSuccessStatusCode)
        {
            var mod = await response.Content.ReadFromJsonAsync<ModPortalMod>();
            if (mod != null)
            {
                return mod;
            }
        }

        throw new Exception("Mod could not be found");
    }
    
    public async void Login(string username, string password)
    {
        var httpClient = new HttpClient();
        var requestParameters = new LoginRequest()
        {
            Username = username,
            Password = password
        };

        var uri = new Uri("https://auth.factorio.com/api-login");
        var response = await httpClient.PostAsync(uri, new StringContent(JsonSerializer.Serialize(requestParameters)));
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Login failed");
        }
    }
    
    public void SetAuthentication(string username, string token)
    {
        _username = username;
        _token = token;
    }
}