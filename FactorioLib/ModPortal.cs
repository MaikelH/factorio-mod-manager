using System.Net.Http.Json;
using FactorioLib.Types;

namespace FactorioLib;

public class ModPortal
{
    private readonly Uri _baseUrl = new("https://mods.factorio.com/api/mods");
    private readonly HttpClient _httpClient;

    public ModPortal()
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = _baseUrl
        };
    }
    
    public async Task<ModListResponse> GetMods()
    {
        var response = await _httpClient.GetFromJsonAsync<ModListResponse>("");
        if (response != null)
        {
            return response;
        }

        throw new Exception("response is null or invalid");
    }    
}