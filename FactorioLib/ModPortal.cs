﻿using System.Net.Http.Json;
using System.Web;
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
    
    public async Task<ModListResponse> GetMods(ModsRequestParameters parameters)
    {
        var uriBuilder = new UriBuilder(_baseUrl);
        var queryParams = parameters.GetQueryParameters();
        uriBuilder.Query = queryParams.ToString();

        var response = await _httpClient.GetFromJsonAsync<ModListResponse>(uriBuilder.ToString());
        if (response != null)
        {
            return response;
        }

        throw new Exception("response is null or invalid");
    }    
}