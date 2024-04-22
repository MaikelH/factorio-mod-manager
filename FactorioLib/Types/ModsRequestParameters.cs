using System.Collections.Specialized;
using System.Web;

namespace FactorioLib.Types;

public class ModsRequestParameters
{
    public bool? HideDeprecated { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public Sort Sort { get; set; } = Sort.Name;
    public SortOrder SortOrder { get; set; } = SortOrder.Desc;
    public String[] NameList { get; set; } = [];
    public FactorioVersion Version { get; set; } = FactorioVersion.V1_1;
    
    public bool ReturnAll { get; set; } = false;

    public string GetQueryString()
    {
        var queryParams = HttpUtility.ParseQueryString("");

        if(HideDeprecated != null)
        {
            queryParams["hide_deprecated"] = HideDeprecated.ToString();
        }
        if (Page != null)
        {
            queryParams["page"] = Page.ToString();
        }
        if(ReturnAll)
        {
            queryParams["page_size"] = "max";
        }
        else
        {
            if(PageSize != null)
            {
                queryParams["page_size"] = PageSize.ToString();
            }
        }

        queryParams["sort"] =Sort.ToCustomString();
        queryParams["sort_order"] = SortOrder.ToCustomString();
        queryParams["version"] = Version.ToCustomString();
        string queryString = queryParams.ToString();
        if (queryString == null)
        {
            return "";
        }

        // Namelist is a special case because the comma separated list cannot be URL encoded (encodes the comma character).
        if (NameList.Length > 0)
        {
            queryString += "&namelist=" + string.Join(",", NameList);
        }
        return queryString;
    }
}

public enum SortOrder
{
    Asc,
    Desc
}

public enum Sort
{
    Name,
    CreatedAt,
    UpdatedAt
}

public enum FactorioVersion
{
    V1_1,
    V1_0,
    V0_18,
    V0_17,
    V0_16,
    V0_15,
    V0_14,
    V0_13,
}