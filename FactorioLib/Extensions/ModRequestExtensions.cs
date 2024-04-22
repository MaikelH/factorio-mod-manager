namespace FactorioLib.Types;

public static class ModRequestExtensions
{
    public static string ToCustomString(this Sort sort)
    {
        switch (sort)
        {
            case Sort.Name:
                return "name";
            case Sort.CreatedAt:
                return "created_at";
            case Sort.UpdatedAt:
                return "updated_at";
            default:
                throw new ArgumentOutOfRangeException(nameof(sort), sort, null);
        }
    }
}

public static class SortOrderExtensions
{
    public static string ToCustomString(this SortOrder sortOrder)
    {
        switch (sortOrder)
        {
            case SortOrder.Asc:
                return "asc";
            case SortOrder.Desc:
                return "desc";
            default:
                throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null);
        }
    }
}

public static class FactorioVersionExtensions
{
    public static string ToCustomString(this FactorioVersion factorioVersion)
    {
        switch (factorioVersion)
        {
            case FactorioVersion.V1_1:
                return "1.1";
            case FactorioVersion.V1_0:
                return "1.0";
            case FactorioVersion.V0_18:
                return "0.18";
            case FactorioVersion.V0_17:
                return "0.17";
            case FactorioVersion.V0_16:
                return "0.16";
            case FactorioVersion.V0_15:
                return "0.15";
            case FactorioVersion.V0_14:
                return "0.14";
            case FactorioVersion.V0_13:
                return "0.13";
            default:
                throw new ArgumentOutOfRangeException(nameof(factorioVersion), factorioVersion, null);
        }
    }
}